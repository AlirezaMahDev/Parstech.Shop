using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranOrderCreateCommandHandler : IRequestHandler<RahkaranOrderCreateCommandReq, RahkaranOrderDto>
{
    private string _connectionString;
    private IMapper _mapper;
    private IMediator _mediator;

    public RahkaranOrderCreateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<RahkaranOrderDto> Handle(RahkaranOrderCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        RahkaranOrder? item = _mapper.Map<RahkaranOrder>(request.dto);

        string query =
            $"INSERT INTO dbo.RahkaranOrder(OrderId,RahkaranPishNumber,RahakaranFactorNumber,RahakaranFactorSerial) VALUES ({item.OrderId},'{item.RahkaranPishNumber}','{item.RahakaranFactorNumber}','{item.RahakaranFactorSerial}')";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranOrderDto>(item);
    }
}