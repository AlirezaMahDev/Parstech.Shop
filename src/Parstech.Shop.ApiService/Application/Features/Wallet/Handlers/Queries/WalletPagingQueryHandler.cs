using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Wallet;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;

namespace Shop.Application.Features.Wallet.Handlers.Queries
{
    public class WalletPagingQueryHandler : IRequestHandler<WalletPagingQueryReq, PagingDto>
    {
        private readonly IWalletRepository _walletRep;
        private readonly IMapper _mapper;
        private readonly string _connectionString;

        public WalletPagingQueryHandler(IWalletRepository walletRep,
            IMapper mapper,
           IConfiguration configuration)
        {
            _walletRep = walletRep;
            _mapper = mapper;
           
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        public async Task<PagingDto> Handle(WalletPagingQueryReq request, CancellationToken cancellationToken)
        {
            
            
            int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

            var query = $"SELECT dbo.[User].UserName, dbo.UserBilling.FirstName, dbo.UserBilling.LastName, dbo.Wallets.WalletId, dbo.Wallets.UserId, dbo.Wallets.Amount, dbo.Wallets.Fecilities, dbo.Wallets.OrgCredit, dbo.Wallets.Coin, dbo.Wallets.IsBlock FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId INNER JOIN dbo.Wallets ON dbo.[User].Id = dbo.Wallets.UserId ORDER BY dbo.Wallets.WalletId Desc OFFSET {skip} ROWS FETCH NEXT {request.Parameter.TakePage} ROWS ONLY";
            var wallets = DapperHelper.ExecuteCommand<List<WalletDto>>(_connectionString, conn => conn.Query<WalletDto>(query).ToList());


            foreach (var item in wallets)
            {
                item.FullName = $"{item.FirstName} {item.LastName}";
            }

            IQueryable<WalletDto> result = wallets.AsQueryable();

            PagingDto response = new PagingDto();

            response.CurrentPage = request.Parameter.CurrentPage;

            var ListCount = _walletRep.GetCountOfWallets();
            response.PageCount = ListCount / request.Parameter.TakePage;


            response.List = result.ToArray();


            if (request.Parameter.UserId!=0)
            {
                var Allquery = $"SELECT dbo.Wallets.*, dbo.[User].UserName, dbo.UserBilling.FirstName, dbo.UserBilling.LastName FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId INNER JOIN dbo.Wallets ON dbo.[User].Id = dbo.Wallets.UserId ";
                var AllList = DapperHelper.ExecuteCommand<List<WalletDto>>(_connectionString, conn => conn.Query<WalletDto>(Allquery).ToList());
                foreach (var item in AllList)
                {
                    item.FullName = $"{item.FirstName} {item.LastName}";
                }

                //var searched = AllList.Where(p =>
                //(p.FullName.Contains( request.Parameter.Filter))||
                //    (p.FirstName.Contains(request.Parameter.Filter) ||
                //     (p.LastName.Contains(request.Parameter.Filter))||
                //     (p.UserName .Contains(request.Parameter.Filter))

                //     )).ToList();
                var searched = AllList.Where(p =>p.UserId== request.Parameter.UserId).ToList();
                

                   

                response.CurrentPage =1;

                
                response.PageCount = 0;


                response.List = searched.ToArray();

                result = wallets.AsQueryable();
            }






            

            return response;

        }
    }
}
