using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.User.Queries;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class GetUsersForSelectListQueryHandler : IRequestHandler<GetUsersForSelectListQueryReq, List<UserForSelectListDto>>
    {
        private readonly IUserQueries _userQueries;
        private readonly string _connectionString;
        public GetUsersForSelectListQueryHandler(IUserQueries userQueries,IConfiguration configuration)
        {
            _userQueries=userQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<List<UserForSelectListDto>> Handle(GetUsersForSelectListQueryReq request, CancellationToken cancellationToken)
        {
            
            var list = DapperHelper.ExecuteCommand<List<UserForSelectListDto>>(_connectionString, conn => conn.Query<UserForSelectListDto>(_userQueries.GetAllUsers).ToList());
            return list;
        }
    }
}
