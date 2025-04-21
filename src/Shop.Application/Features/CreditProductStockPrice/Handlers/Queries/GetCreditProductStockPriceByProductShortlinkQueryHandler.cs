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
    public class GetCreditProductStockPriceByProductShortlinkQueryHandler : IRequestHandler<GetCreditProductStockPriceByProductShortlinkQueryReq, List<CreditProductStockPriceDto>?>
    {
        private string _connectionstring;
        public GetCreditProductStockPriceByProductShortlinkQueryHandler(IConfiguration configuration)
        {
            _connectionstring = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<List<CreditProductStockPriceDto>?> Handle(GetCreditProductStockPriceByProductShortlinkQueryReq request, CancellationToken cancellationToken)
        {
            var query = $"select p.Name,p.Code,p.VariationName,ps.SalePrice,ps.DiscountPrice, c.* from Product as p inner join ProductStockPrice as ps on p.Id=ps.ProductId inner join CreditProductStockPrice as c on ps.Id=c.ProductStockPriceId where p.ShortLink='{request.shortLink}' and c.Active=1";
            var result = DapperHelper.ExecuteCommand<List<CreditProductStockPriceDto>>(_connectionstring, con => con.Query<CreditProductStockPriceDto>(query).ToList());
            return result;
        }
    }
}
