using AutoMapper;

using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

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
    public async Task<RahkaranProductDto> Handle(RahkaranProductUpdateCommandReq request, CancellationToken cancellationToken)
    {

        var item = _mapper.Map<Domain.Models.RahkaranProduct>(request.dto);

        var query = $"UPDATE dbo.RahkaranProduct SET RahkaranProductId='{item.RahkaranProductId}',RahkaranUnitId={item.RahkaranUnitId} Where ProductId={item.ProductId};";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranProductDto>(item);
    }
}