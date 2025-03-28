﻿using AutoMapper;

using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

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