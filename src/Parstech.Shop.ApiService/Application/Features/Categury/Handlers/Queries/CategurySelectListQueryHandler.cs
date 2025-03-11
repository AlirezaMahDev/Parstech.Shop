using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Categury.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Categury.Handlers.Queries
{
    public class CategurySelectListQueryHandler : IRequestHandler<CategurySelectListQueryReq, List<CategurySelectDto>>
    {
        #region Constractor
        private readonly string _connectionString;

        public CategurySelectListQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        public async Task<List<CategurySelectDto>> Handle(CategurySelectListQueryReq request, CancellationToken cancellationToken)
        {
            var query = "select c.GroupId,c.GroupTitle,c.isParnet from Categury as c";
            return DapperHelper.ExecuteCommand<List<CategurySelectDto>>(_connectionString, conn => conn.Query<CategurySelectDto>(query).ToList());

        }
    }
}
