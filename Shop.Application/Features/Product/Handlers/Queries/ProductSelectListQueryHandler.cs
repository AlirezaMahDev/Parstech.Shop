using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ProductSelectListQueryHandler : IRequestHandler<ProductSelectListQueryReq, List<ProductSelectDto>>
    {
        #region Constractor
        private readonly string _connectionString;

        public ProductSelectListQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        public async Task<List<ProductSelectDto>> Handle(ProductSelectListQueryReq request, CancellationToken cancellationToken)
        {
            var query = "select p.Id,p.Code, p.Name as ProductName from Product as p WHERE p.TypeId!=3";
            return DapperHelper.ExecuteCommand<List<ProductSelectDto>>(_connectionString, conn => conn.Query<ProductSelectDto>(query).ToList());
        
        }
    }
}
