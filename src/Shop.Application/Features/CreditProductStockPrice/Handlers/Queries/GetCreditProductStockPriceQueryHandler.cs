using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.Features.CreditProductStockPrice.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Handlers.Queries
{
    public class GetCreditProductStockPriceQueryHandler : IRequestHandler<GetCreditProductStockPriceQueryReq, CreditProductStockPriceDto>
    {
       
        private string _connectionstring;
        public GetCreditProductStockPriceQueryHandler(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("DatabaseConnection");
        }

        
        
        public async Task<CreditProductStockPriceDto?> Handle(GetCreditProductStockPriceQueryReq request, CancellationToken cancellationToken)
        {
            if (request.id != null)
            {
                var query = $"select c.* ,ps.SalePrice,ps.DiscountPrice ,p.Name,p.code from CreditProductStockPrice as c LEFT join productstockPrice as ps on c.ProductStockPriceId=ps.Id LEFT join Product as p on ps.ProductId=p.Id where c.Id={request.id}";
                var result = DapperHelper.ExecuteCommand<CreditProductStockPriceDto>(_connectionstring, con => con.Query<CreditProductStockPriceDto>(query).FirstOrDefault());
                return result;
            }
            var res = new CreditProductStockPriceDto();
            res.ProductStockPriceId = request.productStocPriceId;
            return res;
        }
    }
}
