using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.User;
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class RahkaranOrderReadCommandHandler : IRequestHandler<RahkaranOrderReadCommandReq, RahkaranOrderDto>
    {

        private string _connectionString;
        

        public RahkaranOrderReadCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }


        public async Task<RahkaranOrderDto> Handle(RahkaranOrderReadCommandReq request, CancellationToken cancellationToken)
        {
            var query = $"select* from dbo.RahkaranOrder where dbo.RahkaranOrder.OrderId={request.id}";
            var item=DapperHelper.ExecuteCommand<RahkaranOrderDto>(_connectionString, conn=>conn.Query<RahkaranOrderDto>(query).FirstOrDefault());
            return item;
        }
    }
}
