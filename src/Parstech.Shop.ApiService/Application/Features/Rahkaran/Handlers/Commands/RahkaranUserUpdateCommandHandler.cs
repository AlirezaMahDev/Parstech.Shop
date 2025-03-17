using AutoMapper;

using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranUserUpdateCommandHandler : IRequestHandler<RahkaranUserUpdateCommandReq, RahkaranUserDto>
{
    private string _connectionString;
    private IMapper _mapper;
    private IMediator _mediator;

    public RahkaranUserUpdateCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<RahkaranUserDto> Handle(RahkaranUserUpdateCommandReq request, CancellationToken cancellationToken)
    {
        RahkaranUser? item = _mapper.Map<RahkaranUser>(request.dto);
        string query =
            $"UPDATE dbo.RahkaranUser SET RahkaranUserId='{item.RahkaranUserId}' Where UserId={item.UserId}";
        DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query(query));

        return _mapper.Map<RahkaranUserDto>(item);
    }
}