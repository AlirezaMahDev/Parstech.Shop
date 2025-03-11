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
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class RahkaranOrderUpdateCommandHandler : IRequestHandler<RahkaranOrderUpdateCommandReq, RahkaranOrderDto>
    {


        private string _connectionString;
        private IMapper _mapper;
        private IMediator _mediator;

        public RahkaranOrderUpdateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<RahkaranOrderDto> Handle(RahkaranOrderUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Models.RahkaranOrder>(request.dto);

           var current=await _mediator.Send(new RahkaranOrderReadCommandReq(request.dto.OrderId));
            if(current != null)
            {
                var query = $"UPDATE dbo.RahkaranOrder SET RahkaranPishNumber='{item.RahkaranPishNumber}', RahakaranFactorNumber='{item.RahakaranFactorNumber}',RahakaranFactorSerial='{item.RahakaranFactorSerial}' Where OrderId={item.OrderId};";
                DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));
            }
            else
            {
               await _mediator.Send(new RahkaranOrderCreateCommandReq(request.dto));
            }

            return _mapper.Map<RahkaranOrderDto>(item);
        }
    }
}
