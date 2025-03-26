using AutoMapper;

using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

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
    public async Task<RahkaranOrderDto> Handle(RahkaranOrderCreateCommandReq request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Domain.Models.RahkaranOrder>(request.dto);

        var query = $"INSERT INTO dbo.RahkaranOrder(OrderId,RahkaranPishNumber,RahakaranFactorNumber,RahakaranFactorSerial) VALUES ({item.OrderId},'{item.RahkaranPishNumber}','{item.RahakaranFactorNumber}','{item.RahakaranFactorSerial}')";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranOrderDto>(item);
    }
}