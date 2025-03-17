using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranProductCreateCommandHandler : IRequestHandler<RahkaranProductCreateCommandReq, RahkaranProductDto>
{
    private string _connectionString;
    private IMapper _mapper;
    private IMediator _mediator;

    public RahkaranProductCreateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<RahkaranProductDto> Handle(RahkaranProductCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        RahkaranProduct? item = _mapper.Map<RahkaranProduct>(request.dto);


        string query =
            $"INSERT INTO dbo.RahkaranProduct(ProductId,RahkaranProductId,RahkaranUnitId) VALUES ({item.ProductId},'{item.RahkaranProductId}',{item.RahkaranUnitId})";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));


        return _mapper.Map<RahkaranProductDto>(item);
    }
}