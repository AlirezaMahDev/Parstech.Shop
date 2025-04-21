using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    
    public class DicountProductSelectListQueryHandler : IRequestHandler<DicountProductSelectListQueryReq, List<ProductSelectDto>>
    {
        #region Constractor
        private readonly string _connectionString;

        public DicountProductSelectListQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        public async Task<List<ProductSelectDto>> Handle(DicountProductSelectListQueryReq request, CancellationToken cancellationToken)
        {
            var AllListquery = $"SELECT p.Id,p.Name as ProductName,p.Code  FROM dbo.ProductStockPrice as ps INNER JOIN dbo.Product as p ON p.Id = ps.ProductId where ps.DiscountPrice!=0  and p.IsActive=1      and p.IsActive=1 ORDER BY CreateDate Desc ";

            return DapperHelper.ExecuteCommand<List<ProductSelectDto>>(_connectionString, conn => conn.Query<ProductSelectDto>(AllListquery).ToList());

        }
    }
}
