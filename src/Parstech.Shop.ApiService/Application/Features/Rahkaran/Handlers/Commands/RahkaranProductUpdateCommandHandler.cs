using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranProductUpdateCommandHandler : IRequestHandler<RahkaranProductUpdateCommandReq, RahkaranProductDto>
{
    private string _connectionString;
    private IMapper _mapper;
    private IMediator _mediator;

    public RahkaranProductUpdateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<RahkaranProductDto> Handle(RahkaranProductUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        RahkaranProduct? item = _mapper.Map<Domain.Models.RahkaranProduct>(request.dto);

        string? query =
            $"UPDATE dbo.RahkaranProduct SET RahkaranProductId='{item.RahkaranProductId}',RahkaranUnitId={item.RahkaranUnitId} Where ProductId={item.ProductId};";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranProductDto>(item);
    }
}