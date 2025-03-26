using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;
using Parstech.Shop.Context.Application.Convertor;

using System.Text.RegularExpressions;
using Xceed.Document.NET;
using Xceed.Words.NET;

using System.Globalization;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class WalletTransactionRepository : GenericRepository<WalletTransaction>, IWalletTransactionRepository
{
    private readonly DatabaseContext _context;
    private readonly IWalletTypesRepository _walletTypesRepo;
    private readonly IWalletRepository _walletRep;
    private readonly IMapper _mapper;

    public WalletTransactionRepository(DatabaseContext context, IWalletTypesRepository walletTypesRepo, IWalletRepository walletRep, IMapper mapper) : base(context)
    {
        _context = context;
        _walletTypesRepo = walletTypesRepo;
        _walletRep = walletRep;
        _mapper = mapper;
    }



    public async Task<bool> WalletIsWerifyForNewFecilities(int WalletId, int TransactionTypeId, string TypeName)
    {
        if (await _context.WalletTransactions.AnyAsync(u => u.WalletId == WalletId && u.Active == true && u.Type == TypeName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public async Task<WalletTransaction> GetParentFecilities(int parentId)
    {
        var item =await _context.WalletTransactions.FindAsync(parentId);
        return item;
    }
    public async Task<List<WalletTransaction>> GetAghsatByParentId(int parentId,int typeId)
    {
        if (typeId != 0)
        {
            var list = await _context.WalletTransactions.Where(u => u.ParentFecilitiesId == parentId && u.TypeId == typeId).ToListAsync();
            return list;
        }
        else
        {
            var list = await _context.WalletTransactions.Where(u => u.ParentFecilitiesId == parentId).ToListAsync();
            return list;
        }
            
    }

    public FecilitiesDto GenerateNewFesilities(FecilitiesDto request)
    {
        double B1 = request.Price;
        double B2 = request.Sud;
        double B3 = request.GhestCount;
        double EachGhest = 0;
        if (B2 == 0) {
            EachGhest =  B1 / B3;
        }
        else
        {
            EachGhest = ((B2 / 1200) * Math.Pow((1 + B2 / 1200), B3) * B1) / (Math.Pow((1 + B2 / 1200), B3) - 1);
        }

        request.Ghest = Math.Round(EachGhest);
            
        request.Karmozd = request.Price * 0.02;
        request.Karmozd = (request.Price /100)*request.KarmozdPersent;
        request.TotalPrice = (request.GhestCount * request.Ghest) + request.Karmozd;
        request.ParentFecilitiesId = request.ParentFecilitiesId;
        return request;
    }
    public async Task<FecilitiesDto> CreateNewFesilities(FecilitiesDto request)
    {
        var Today = DateTime.Now.ToShamsi();
        string GhestDate =null;
        string[] std = Today.Split('/');
        var year = int.Parse(std[0]);
        var month = int.Parse(std[1]);
        var day = int.Parse(std[2]);

        if (day >= 10)
        {
            month += 1;
        }
            
        List<string> Dates = new();

        for(int i =0; i < request.GhestCount; i++)
        {
                
            if (month > 12)
            {
                month = 1;
                year += 1;
            }

            GhestDate = $"{year}/{month}/25";
            Dates.Add(GhestDate);
            month+=1;
        }
            


            




        Random random = new();
        var tackingCode = random.Next(10000000, 99999999);
        var Description = $"شناسه تسهیلات: {request.ParentFecilitiesId} | {request.OrderCode} اقساط سفارش ";
        //var currentTransaction = new WalletTransaction();

        foreach (var item in Dates)
        {
            string[] fgd = item.Split('/');
            var date = new DateTime(int.Parse(fgd[0]),
                int.Parse(fgd[1]),
                int.Parse(fgd[2]),
                new PersianCalendar()
            );

            WalletTransaction transaction = new()
            {
                WalletId = request.WalletId,
                CreateDate = date,
                Price = Convert.ToInt32(request.Ghest),
                Type = request.Type,
                TypeId = 6,
                TrackingCode = tackingCode++.ToString(),
                Description = Description.ToString(),
                ParentFecilitiesId=request.ParentFecilitiesId,
                FileName = "",
            };
            transaction.ExpireDate = transaction.CreateDate.AddDays(7);


            await AddAsync(transaction);
        }
           

        //request.Serial = Description.ToString();

        //var Wallet = await _walletRep.GetAsync(request.WalletId);
        //Wallet.Fecilities += Convert.ToInt64(request.Price);
        //await _walletRep.UpdateAsync(Wallet);
        //currentTransaction.FileName = GenerateWordOfFecilities(request);
        //await UpdateAsync(currentTransaction);
        return request;
    }


    public string GenerateWordOfFecilities(FecilitiesDto item)
    {
        var source = "wwwroot/Shared/Factors/fecilitiest.docx";
        var filename = "";
        var result = "";

        var wallet = _context.Wallets.Find(item.WalletId);
        var userbilling = _context.UserBillings.FirstOrDefault(u => u.UserId == wallet.UserId);
        var Transactions = _context.WalletTransactions.Where(u => u.Description == item.Serial).ToList();

        using (var document = DocX.Load(source))
        {

            //var date = WordDate(order.CreateDate.ToShamsi());
            document.ReplaceText("{سریال}", item.Serial, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{تاریخ}", DateTime.Now.ToShamsi(), false, RegexOptions.IgnoreCase);
            document.ReplaceText("{شرکت}", userbilling.FirstName, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{اقتصادی}", userbilling.EconomicCode, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{تلفن}", userbilling.Phone, false, RegexOptions.IgnoreCase);
            //document.ReplaceText("{ثبت}", order.Order.OrderCode, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{نشانی}", userbilling.Address, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{ملی}", userbilling.NationalCode, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{پستی}", userbilling.PostCode, false, RegexOptions.IgnoreCase);
            //document.ReplaceText("{نمابر}", order.Order.OrderCode, false, RegexOptions.IgnoreCase);


            document.ReplaceText("{مبلغ}", item.Price.ToString, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{نرخ}", item.Sud.ToString, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{اقساط}", item.GhestCount.ToString, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{قسط}", item.Ghest.ToString, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{کامزد}", item.Karmozd.ToString, false, RegexOptions.IgnoreCase);
            document.ReplaceText("{کل}", item.TotalPrice.ToString, false, RegexOptions.IgnoreCase);

            if (Transactions.Count() > 0)
            {
                // جستجوی جدول مورد نظر برای پر کردن
                var table = document.Tables.FirstOrDefault(t => t.Rows[0].Cells[0].Paragraphs.First().Text == "*");
                if (table != null)
                {
                    // پر کردن سلول‌های جدول با داده‌های مورد نظر
                    for (int i = 0; i < Transactions.Count; i++)
                    {
                        var row = i + 1;
                        var newRow = table.InsertRow(table.Rows.First());
                        newRow.Cells[0].Paragraphs.First().ReplaceText("*", row.ToString(), false, RegexOptions.IgnoreCase);
                        newRow.Cells[0].Paragraphs.First().Alignment = Alignment.right;
                        newRow.Cells[1].Paragraphs.First().ReplaceText("اقساط", Transactions[i].Price.ToString, false, RegexOptions.IgnoreCase);
                        newRow.Cells[2].Paragraphs.First().ReplaceText("سررسید", Transactions[i].CreateDate.ToShamsi(), false, RegexOptions.IgnoreCase);
                        newRow.Cells[2].Paragraphs.First().Alignment = Alignment.left;
                        newRow.Cells[3].Paragraphs.First().ReplaceText("مهلت پرداخت", Transactions[i].ExpireDate.Value.ToShamsi(), false, RegexOptions.IgnoreCase);
                        newRow.Cells[4].Paragraphs.First().ReplaceText("وضعیت", Transactions[i].TypeId.ToString, false, RegexOptions.IgnoreCase);
                        newRow.Cells[5].Paragraphs.First().ReplaceText("کد پیگیری", Transactions[i].TrackingCode, false, RegexOptions.IgnoreCase);

                    }
                }


                Random random = new();
                var title = random.Next(10000, 99999);
                filename = $"wwwroot/Shared/Factors/{item.Serial}({title}).doc";
                result = $"Shared/Factors/{item.Serial}({title}).doc";

                foreach (var data in document.Paragraphs)
                {
                    data.Direction = Direction.RightToLeft;
                }
            }

            // ذخیره فایل Word

            document.AddProtection(EditRestrictions.readOnly);

            document.SaveAs(filename);



        }



        return result;
    }
    public async Task<WalletTransaction> GetLastOfOrder(string orderCode)
    {
        var item = await _context.WalletTransactions.OrderBy(u => u.CreateDate).LastOrDefaultAsync(u => u.Description == orderCode);
        return item;
    }
    public async Task<List<WalletTransaction>> GetTransactionsByParentId(int parentId)
    {
        var list = await _context.WalletTransactions.Where(u=>u.ParentFecilitiesId==parentId).OrderBy(u => u.CreateDate).ToListAsync();
        return list;
    }

    public async Task<WalletTransaction> GetActiveAghsat(int walletId, string type)
    {
        var item=await _context.WalletTransactions.FirstOrDefaultAsync(u => u.WalletId == walletId && u.Type == type && u.Active==true);
           
        return item;
    }

    public async Task<WalletTransaction> GetTransactionByTrackingCode(string trackingCode)
    {
        var item = await _context.WalletTransactions.FirstOrDefaultAsync(u=>u.TrackingCode==trackingCode);
        return item;
    }
        
    public async Task<bool> ExistTransactionForUser(int UserId,string TracsactionCode)
    {
        var wallet =await _context.Wallets.FirstOrDefaultAsync(u => u.UserId == UserId);
        if (await _context.WalletTransactions.AnyAsync(u => u.WalletId == wallet.WalletId && u.TrackingCode == TracsactionCode))
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }
}