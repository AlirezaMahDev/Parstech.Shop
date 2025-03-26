using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.WalletTransaction.Queries;
using Parstech.Shop.Context.Application.DTOs.WalletTransaction;
using Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

using System.Globalization;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Handlers.Queries;

public class ReportOfActiveCreditQueryHandler : IRequestHandler<ReportOfActiveCreditQueryReq, WalletTransactionPagingDto>
{

    private readonly ITransactionQueries _transactionQuery;
    private readonly IMapper _mapper;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly string _connectionString;
    public ReportOfActiveCreditQueryHandler(ITransactionQueries transactionQuery, IConfiguration configuration, IWalletTransactionRepository walletTransactionRep,IMapper mapper)
    {
        _transactionQuery = transactionQuery;
        _walletTransactionRep= walletTransactionRep;
        _mapper= mapper;    
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<WalletTransactionPagingDto> Handle(ReportOfActiveCreditQueryReq request, CancellationToken cancellationToken)
    {
        WalletTransactionPagingDto result = new();
        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;
        var list = DapperHelper.ExecuteCommand<List<WalletTransactionReportDto>>(_connectionString, conn => conn.Query<WalletTransactionReportDto>(_transactionQuery.GetActiveCredit).ToList());
            
        foreach (var item in list)
        {
            item.CreateDateShamsi = item.CreateDate.ToShamsi();
            var childs =await _walletTransactionRep.GetTransactionsByParentId(item.Id);
                
            var first=childs.FirstOrDefault();
            var last = childs.LastOrDefault();
            item.FirstDate = first.CreateDate.ToShamsi();
            item.LastDate = last.CreateDate.ToShamsi();
        }

        if (!string.IsNullOrEmpty(request.parameter.UserFilter))
        {
            list = list.Where(u => u.UserName == request.parameter.UserFilter).ToList();
        }

        if (!string.IsNullOrEmpty(request.parameter.WalletType))
        {
            list = list.Where(u => u.Type == request.parameter.WalletType).ToList();
        }

        if (request.parameter.TransactionType != 0)
        {
            list = list.Where(u => u.TypeId == request.parameter.TransactionType).ToList();
        }

        if (!string.IsNullOrEmpty(request.parameter.FromDate))
        {
            request.parameter.FromDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.parameter.FromDate);
            string[] std = request.parameter.FromDate.Split('/');
            var az = new DateTime(int.Parse(std[0]),
                int.Parse(std[1]),
                int.Parse(std[2]),
                new PersianCalendar()
            );
            list = list.Where(p => (p.CreateDate >= az)).ToList();
        }
        if (!string.IsNullOrEmpty(request.parameter.ToDate))
        {
            request.parameter.ToDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.parameter.ToDate);
            string[] edd = request.parameter.ToDate.Split('/');
            var ta = new DateTime(int.Parse(edd[0]),
                int.Parse(edd[1]),
                int.Parse(edd[2]),
                new PersianCalendar()
            );
            list = list.Where(p => (p.CreateDate <= ta)).ToList();
        }

        if (request.parameter.TakePage == -1)
        {
            result.walletTransactions = list;
            return result;
        }
        result.CurrentPage = request.parameter.CurrentPage;

        result.PageCount = (list.Count() / request.parameter.TakePage) + 1;


        result.List = list.Skip(skip).Take(request.parameter.TakePage).ToArray();
        return result;
    }
}