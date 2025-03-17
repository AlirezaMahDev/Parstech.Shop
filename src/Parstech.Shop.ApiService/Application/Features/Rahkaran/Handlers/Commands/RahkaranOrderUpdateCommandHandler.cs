using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

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

    public async Task<RahkaranOrderDto> Handle(RahkaranOrderUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        RahkaranOrder? item = _mapper.Map<RahkaranOrder>(request.dto);

        RahkaranOrderDto? current = await _mediator.Send(new RahkaranOrderReadCommandReq(request.dto.OrderId));
        if (current != null)
        {
            string query =
                $"UPDATE dbo.RahkaranOrder SET RahkaranPishNumber='{item.RahkaranPishNumber}', RahakaranFactorNumber='{item.RahakaranFactorNumber}',RahakaranFactorSerial='{item.RahakaranFactorSerial}' Where OrderId={item.OrderId};";
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));
        }
        else
        {
            await _mediator.Send(new RahkaranOrderCreateCommandReq(request.dto));
        }

        return _mapper.Map<RahkaranOrderDto>(item);
    }
}