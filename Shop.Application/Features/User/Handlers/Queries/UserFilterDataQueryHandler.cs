using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class UserFilterDataQueryHandler : IRequestHandler<UserFilterDataQueryReq, List<UserFilterDto>>
    {
        private readonly string _connectionString;
        public UserFilterDataQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");   
        }
        public async Task<List<UserFilterDto>> Handle(UserFilterDataQueryReq request, CancellationToken cancellationToken)
        {
            var query = "select dbo.[User].Id,dbo.[User].UserName,dbo.UserBilling.FirstName,dbo.UserBilling.LastName,dbo.UserBilling.EconomicCode,dbo.UserBilling.NationalCode,dbo.UserBilling.Mobile from dbo.[User] inner join dbo.UserBilling on dbo.[User].Id=dbo.UserBilling.UserId";
            var res = DapperHelper.ExecuteCommand<List<UserFilterDto>>(_connectionString, conn => conn.Query<UserFilterDto>(query).ToList());
            return res;

        }
    }
}
