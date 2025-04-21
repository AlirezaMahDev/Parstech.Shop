using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Brand;
using Shop.Application.Features.Brand.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Brand.Handlers.Queries
{
    public class BrandListForProductsListCateguryQueryHandler : IRequestHandler<BrandListForProductsListCateguryQueryReq, List<BrandDto>>
    {
        private readonly string _connectionString;
        private readonly ICateguryRepository _catRep;
        public BrandListForProductsListCateguryQueryHandler(IConfiguration configuration, ICateguryRepository catRep)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _catRep = catRep;
        }
        public async Task<List<BrandDto>> Handle(BrandListForProductsListCateguryQueryReq request, CancellationToken cancellationToken)
        {
            var categuryIdQuery= new StringBuilder();
            var cat = await _catRep.GetCateguryByLatinName(request.Categury);
            if (cat != null)
            {
                categuryIdQuery.Append($" CateguryId = {cat.GroupId} ");
                if (cat.IsParnet)
                {
                    var cats = await _catRep.GetCateguryByParentId(cat.GroupId, null);
                    foreach (var ca in cats)
                    {
                        categuryIdQuery.Append($"Or CateguryId = {ca.GroupId}");
                    }
                }
                var Sql = $"SELECT DISTINCT b.BrandId,b.BrandTitle FROM dbo.Product p LEFT JOIN dbo.Brand as b ON p.BrandId = b.BrandId WHERE IsActive=1 AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND ( {categuryIdQuery} ))";
                var res = DapperHelper.ExecuteCommand<List<BrandDto>>(_connectionString, con => con.Query<BrandDto>(Sql).ToList());
                return res;
            }
            return new List<BrandDto>();
        }
    }
}
