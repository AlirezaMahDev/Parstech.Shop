using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

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
        RahkaranUser? item = _mapper.Map<RahkaranUser>(request.dto);
        string query =
            $"INSERT INTO dbo.RahkaranUser(UserId,RahkaranUserId) VALUES ({item.UserId},'{item.RahkaranUserId}')";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranUserDto>(item);
    }
}