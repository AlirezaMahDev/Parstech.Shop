using Azure.Core;
using Dapper;
using Dto.Response.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Commands;
using Shop.Application.Dapper.ProductStockPrice.Commands;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class ProductStockPriceRepository : GenericRepository<ProductStockPrice>, IProductStockPriceRepository
    {
        private readonly DatabaseContext _context;
        
        private readonly IProductStockPriceCommand _commandText;
        private readonly string _connectionString;
        public ProductStockPriceRepository(DatabaseContext context,
            IConfiguration configuration, IProductStockPriceCommand commandText) : base(context)
        {
            _context = context;
            _commandText = commandText;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
          
        }

        public async Task<int> GetFirstProductStockPriceIdFromProductId(int productId)
        {

            if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == productId))
            {
                var pr = await _context.ProductStockPrices.FirstOrDefaultAsync(u => u.ProductId == productId && u.SalePrice > 0 && u.Quantity > 0);
                if (pr == null)
                {
                    pr = await _context.ProductStockPrices.FirstOrDefaultAsync(u => u.ProductId == productId);
                }
                return pr.Id;
            }
            else
            {
                return 0;
            }
        }
        public async Task<List<ProductStockPrice>> GetAllByProductId(int productId)
        {

            List<ProductStockPrice> result = new List<ProductStockPrice>();
            result = await _context.ProductStockPrices.Where(u => u.ProductId == productId).ToListAsync();
            return result;
        }
        public async Task<ProductStockPrice?> DapperGetProductStockPriceById(int id)
        {
            var query = DapperHelper.ExecuteCommand<ProductStockPrice>(_connectionString, conn => conn.Query<ProductStockPrice>(_commandText.GetProductStockPriceById, new { @Id = id }).SingleOrDefault());

            return query;
        }

        public async Task<ProductStockPrice?> GetProductStockByProductIdAndStoreId(int productId, int storeId)
        {
            var productStockPrice = await _context.ProductStockPrices.FirstOrDefaultAsync(u => u.ProductId == productId && u.StoreId == storeId);
            return productStockPrice;
        }

        public async Task<bool> ExistStockForProductIdAndStore(int productId, int storeId)
        {
            if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == productId && u.StoreId == storeId))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> ExistStockForParentProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product.ParentId == null)
            {
                return true;
            }

            if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == product.ParentId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ProductStockPriceDto>> GetAllStock(int productId, bool UserCateguryExist)
        {
            List<ProductStockPriceDto> Result = new List<ProductStockPriceDto>();
            //var product =await _productRep.GetAsync(productId);
            var mainProductQuery = $"select* from Product where Id={productId} ";
            var product = DapperHelper.ExecuteCommand<ProductDto>(_connectionString, conn => conn.Query<ProductDto>(mainProductQuery).FirstOrDefault());


            var UserCtaeguryCondition = "";
            if (!UserCateguryExist && product.TypeId!=1)
            {
                UserCtaeguryCondition = "AND CateguryOfUserId IS NULL";
            }
            var productStockQuery = $"select* from ProductStockPrice where ProductId={productId} {UserCtaeguryCondition}";
            var prodcuctStocks = DapperHelper.ExecuteCommand<List<ProductStockPriceDto>>(_connectionString, conn => conn.Query<ProductStockPriceDto>(productStockQuery).ToList());
            Result.AddRange(prodcuctStocks);

            var productQuery = $"select * from Product where ParentId={productId} ";
            var childs = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(productQuery).ToList());
            foreach (var item in childs)
            {
                var ChildStockQuery = $"select* from ProductStockPrice where ProductId={item.Id} {UserCtaeguryCondition}";
                var ChildStocks = DapperHelper.ExecuteCommand<List<ProductStockPriceDto>>(_connectionString, conn => conn.Query<ProductStockPriceDto>(ChildStockQuery).ToList());
                Result.AddRange(ChildStocks);
            }

            foreach (var item in Result)
            {
                if (item.DiscountPrice > 0)
                {
                    item.BestPriceForBestStock = item.DiscountPrice;
                }
                else
                {
                    item.BestPriceForBestStock = item.SalePrice;
                }
            }
            return Result;
        }
        public async Task<int?> GetBestStockId(int productId)
        {
            var stocks = await GetAllStock(productId, false);
            var Catstocks = await GetAllStock(productId, true);
            if (stocks.Count == 0 || stocks == null)
            {
               
                return null;
            }
            
            var stock = stocks.Where(u => u.Quantity > 0 && u.SalePrice > 0).OrderBy(u => u.BestPriceForBestStock).FirstOrDefault();
            if (stock != null)
            {
                return stock.Id;
            }
            else
            {
                var notExistStock = stocks.FirstOrDefault();
                if (notExistStock != null)
                {
                    return notExistStock.Id;
                }
                
            }
            return null;
        }

        public async Task<int?> GetBestStockUserCateguryId(int productId)
        {
            var stocks = await GetAllStock(productId, true);
            if (stocks.Count == 0 || stocks == null)
            {
                return null;
            }
            var stock = stocks.Where(u => u.Quantity > 0 && u.SalePrice > 0).OrderBy(u => u.BestPriceForBestStock).FirstOrDefault();
            if (stock != null)
            {
                return stock.Id;
            }
            else
            {
                var notExistStock = stocks.FirstOrDefault();
                if (notExistStock != null)
                {
                    return notExistStock.Id;
                }

            }
            return null;
        }
    }
}
