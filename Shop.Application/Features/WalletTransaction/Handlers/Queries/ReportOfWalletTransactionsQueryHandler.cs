using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.Dapper.WalletTransaction.Queries;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shop.Application.Features.WalletTransaction.Handlers.Queries
{
    public class ReportOfWalletTransactionsQueryHandler : IRequestHandler<ReportOfWalletTransactionsQueryReq, WalletTransactionPagingDto>
    {
        private readonly ITransactionQueries _transactionQuery;
        private readonly string _connectionString;
        public ReportOfWalletTransactionsQueryHandler(ITransactionQueries transactionQuery,IConfiguration configuration)
        {
            _transactionQuery = transactionQuery;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<WalletTransactionPagingDto> Handle(ReportOfWalletTransactionsQueryReq request, CancellationToken cancellationToken)
        {
            WalletTransactionPagingDto result =new WalletTransactionPagingDto();
            int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;
            var list = DapperHelper.ExecuteCommand<List<WalletTransactionReportDto>>(_connectionString, conn => conn.Query<WalletTransactionReportDto>(_transactionQuery.GetAllTransaction).ToList());
            foreach( var item in list )
            {
                item.CreateDateShamsi = item.CreateDate.ToShamsi();
                
            }
            
            if (!string.IsNullOrEmpty( request.parameter.UserFilter))
            {
                list =list.Where(u=>u.UserName==request.parameter.UserFilter).ToList();
            }

            if(!string.IsNullOrEmpty(request.parameter.WalletType))
            {
                list = list.Where(u => u.Type == request.parameter.WalletType).ToList();
            }

            if(request.parameter.TransactionType!=0)
            {
                list = list.Where(u => u.TypeId == request.parameter.TransactionType).ToList();
            }

            if(!string.IsNullOrEmpty(request.parameter.FromDate))
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
            if(!string.IsNullOrEmpty(request.parameter.ToDate))
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

            result.PageCount = (list.Count() / request.parameter.TakePage)+1;


            result.List = list.Skip(skip).Take(request.parameter.TakePage).ToArray();
            return result;
        }
    }
}
