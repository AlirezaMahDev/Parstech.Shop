using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Brand.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserStore.Handlers.Queries
{
   
    public class UserStoresForProductsListCateguryQueryHandler : IRequestHandler<UserStoresForProductsListCateguryQueryReq, List<UserStoreDto>>
    {
        private readonly string _connectionString;
        private readonly ICateguryRepository _catRep;
        public UserStoresForProductsListCateguryQueryHandler(IConfiguration configuration, ICateguryRepository catRep)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _catRep = catRep;
        }
        public async Task<List<UserStoreDto>> Handle(UserStoresForProductsListCateguryQueryReq request, CancellationToken cancellationToken)
        {
            var categuryIdQuery = new StringBuilder();
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
                var Sql = $"SELECT DISTINCT us.Id,us.LatinStoreName,us.StoreName FROM dbo.Product p  inner JOIN dbo.ProductStockPrice as ps ON p.Id = ps.ProductId inner JOIN dbo.UserStore as us ON ps.StoreId = us.Id  WHERE IsActive=1 AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND ( {categuryIdQuery}))";
                //var res = DapperHelper.ExecuteCommand<List<UserStoreDto>>(_connectionString, con => con.Query<UserStoreDto>(Sql).ToList());
                var res = DapperHelper.ExecuteCommand<List<UserStoreDto>>(_connectionString, con => con.Query<UserStoreDto>(Sql).ToList());
                return res;
            }
            return new List<UserStoreDto>();
        }
    }
}
