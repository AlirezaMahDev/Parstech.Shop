import pandas as pd
from fastapi import FastAPI
import logging
import pyodbc
import sys
from sentence_transformers import SentenceTransformer
import chromadb
from chromadb.config import Settings

# تنظیمات لاگینگ
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)

app = FastAPI()

# --- تنظیمات پیکربندی ---
MODEL_NAME = 'sentence-transformers/paraphrase-multilingual-mpnet-base-v2'
DB_CONN_STR = (
    'DRIVER={ODBC Driver 17 for SQL Server};'
    'SERVER=localhost;'
    'DATABASE=parstech;'
    'Trusted_Connection=yes;'
)
CHROMA_DIR = "./chroma_db"

# --- متغیرهای سراسری ---
chroma_client = None
collection = None
products_df = None
model = None
conn = None  # مدیریت اتصال به دیتابیس

# --- توابع کمکی ---
def load_data_and_init_chroma():
    global products_df, chroma_client, collection, model, conn
    
    try:
        # 1. اتصال به دیتابیس
        logging.info("📡 در حال اتصال به دیتابیس...")
        conn = pyodbc.connect(DB_CONN_STR)
        
        # 2. دریافت داده‌های محصولات
        logging.info("🔍 دریافت داده‌های محصولات از دیتابیس...")
        products_df = pd.read_sql("""
            SELECT
                Id,
                Name,
                COALESCE(Description, '') AS Description,
                COALESCE(Keywords, '') AS Keywords,
                IsActive
            FROM Product
            WHERE IsActive = 1
        """, conn)

        # 3. ترکیب متن با وزن دهی به نام محصول
        products_df['combined_text'] = (
            products_df['Name'] + " " +
            products_df['Description'].str[:200] + " " +  # محدودیت طول
            products_df['Keywords']
        )
                
        # 4. راه‌اندازی ChromaDB
        logging.info("🛠️ راه‌اندازی پایگاه داده Chroma...")
        chroma_client = chromadb.Client(Settings(
            persist_directory=CHROMA_DIR,
            is_persistent=True
        ))
        
        # 5. ایجاد/بارگیری کالکشن
        collection = chroma_client.get_or_create_collection(
            name="products",
            metadata={"hnsw:space": "cosine"}
        )
        
        # 6. تولید امبدینگ‌ها
        model = SentenceTransformer(MODEL_NAME)
        embeddings = model.encode(
            products_df['combined_text'].tolist(),
            show_progress_bar=True,
            batch_size=32
        )
        
        # 7. افزودن داده به ChromaDB
        collection.add(
            embeddings=embeddings.tolist(),
            documents=products_df['combined_text'].tolist(),
            ids=[str(id) for id in products_df['Id'].tolist()]
        )
        
        logging.info(f"✅ {collection.count()} محصول با موفقیت ایندکس شد")

    except pyodbc.Error as e:
        logging.error(f"❌ خطای دیتابیس: {str(e)}")
        raise
    except Exception as e:
        logging.error(f"❌ خطای غیرمنتظره: {str(e)}")
        raise
    finally:
        if conn:
            conn.close()

# --- رویدادهای سرور ---
@app.on_event("startup")
async def startup_event():
    try:
        load_data_and_init_chroma()
        logging.info("🚀 سرور با موفقیت راه‌اندازی شد!")
    except Exception as e:
        logging.critical(f"💥 خطای بحرانی: {str(e)}")
        sys.exit(1)

@app.on_event("shutdown")
async def shutdown_event():
    logging.info("🛑 در حال خاموش کردن سرور...")
    # مدیریت منابع ChromaDB
    if chroma_client:
        try:
            chroma_client.reset()  # پاکسازی منابع
        except Exception as e:
            logging.error(f"⚠️ خطا در پاکسازی ChromaDB: {str(e)}")
    logging.info("✅ منابع آزاد شدند")

# --- اندپوینت‌های API ---
# اضافه کردن imports
from persian_spell_checker import PersianSpellChecker

# --- افزودن تنظیمات جدید ---
SPELL_CHECKER = PersianSpellChecker()

# --- اصلاح اندپوینت جستجو ---
@app.post("/search")
async def search_products(query: str, top_k: int = 5):
    try:
        # اعتبارسنجی پارامترها
        if top_k < 1 or top_k > 100:
            return {"error": "مقدار top_k باید بین ۱ تا ۱۰۰ باشد"}
        
        # تصحیح خودکار املایی
        corrected_query = SPELL_CHECKER.spell_corrector(query)
        if corrected_query != query:
            logging.info(f"اصلاح املایی: '{query}' => '{corrected_query}'")
            query = corrected_query
        
        # تقویت خودکار کوئری
        query_booster = {
            "میکروفون": ["صدا", "ضبط", "کپچر", "اسپیکر"],
            "لپتاپ": ["نوت‌بوک", "رایانه همراه", "کامپیوتر"],
            "هدفون": ["هندزفری", "بلوتوث", "گوشی"]
        }
        boosted_query = query.lower()
        for key, values in query_booster.items():
            if key in boosted_query:
                boosted_query += " " + " ".join(values)
        
        # تولید امبدینگ
        query_embedding = model.encode([boosted_query]).tolist()[0]
        
        # جستجو در ChromaDB
        results = collection.query(
            query_embeddings=[query_embedding],
            n_results=top_k * 5
        )
        
        # پردازش نتایج
        final_results = []
        seen_ids = set()
        
        for idx, product_id in enumerate(results['ids'][0]):
            try:
                product = products_df[products_df['Id'] == int(product_id)].iloc[0]
                
                # محاسبه امتیاز
                name_score = int(product['Name'].lower().count(query.lower())) * 3
                desc_score = int(product['Description'].lower().count(query.lower())) * 2
                kw_score = int(product['Keywords'].lower().count(query.lower())) * 1.5
                similarity_score = (name_score + desc_score + kw_score) * 0.8 + (1 - results['distances'][0][idx]) * 0.2
                
                if similarity_score > 0.1 and product['Id'] not in seen_ids:
                    final_results.append({
                        "id": int(product['Id']),
                        "name": str(product['Name']),
                        "score": float(round(similarity_score, 2)),
                        "matches": [
                            f"تطابق در نام ({int(name_score)})",
                            f"تطابق در توضیحات ({int(desc_score)})"
                        ],
                        "original_query": query  # نمایش کوئری اصلاح شده
                    })
                    seen_ids.add(int(product['Id']))
                
                if len(final_results) >= top_k:
                    break
                    
            except Exception as e:
                logging.error(f"خطا در پردازش محصول: {str(e)}")
        
        return {
            "results": final_results[:top_k],
            "corrected_query": corrected_query if corrected_query != query else None
        }
        
    except Exception as e:
        logging.error(f"خطا: {str(e)}")
        return {"error": "خطای سرور"}
        
    

@app.get("/health")
async def health_check():
    return {
        "status": "فعال",
        "products_loaded": len(products_df) if products_df else 0,
        "chroma_status": "فعال" if collection else "غیرفعال",
        "index_size": collection.count() if collection else 0
    }

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(
        "main:app",
        host="0.0.0.0",
        port=8000,
        reload=True,
        timeout_keep_alive=30,
        workers=1
    )