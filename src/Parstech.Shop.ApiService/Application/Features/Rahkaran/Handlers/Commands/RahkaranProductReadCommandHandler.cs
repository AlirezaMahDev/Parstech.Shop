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
    public class RahkaranProductReadCommandHandler : IRequestHandler<RahkaranProductReadCommandReq, RahkaranProductDto>
    {



        private string _connectionString;


        public RahkaranProductReadCommandHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }


        public async Task<RahkaranProductDto> Handle(RahkaranProductReadCommandReq request, CancellationToken cancellationToken)
        {
            var query = $"select* from dbo.RahkaranProduct where dbo.RahkaranProduct.ProductId={request.id}";
            var item = DapperHelper.ExecuteCommand<RahkaranProductDto>(_connectionString, conn => conn.Query<RahkaranProductDto>(query).FirstOrDefault());
            return item;
        }
    }
}
