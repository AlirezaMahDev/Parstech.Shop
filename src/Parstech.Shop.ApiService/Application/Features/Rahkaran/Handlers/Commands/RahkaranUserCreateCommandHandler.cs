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
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.Wallet;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.Wallet.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class RahkaranUserCreateCommandHandler : IRequestHandler<RahkaranUserCreateCommandReq, RahkaranUserDto>
    {
        private string _connectionString;
        private IMapper _mapper;
        private IMediator _mediator;

        public RahkaranUserCreateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<RahkaranUserDto> Handle(RahkaranUserCreateCommandReq request, CancellationToken cancellationToken)
        {
           
            var item = _mapper.Map<Domain.Models.RahkaranUser>(request.dto);
            var query = $"INSERT INTO dbo.RahkaranUser(UserId,RahkaranUserId) VALUES ({item.UserId},'{item.RahkaranUserId}')";
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

            return _mapper.Map<RahkaranUserDto>(item);
        }

       
         
    }
}
