﻿using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Queries;

public class UserStoreOfUserReadQueryHandler : IRequestHandler<UserStoreOfUserReadQueryReq, UserStoreDto>
{
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreOfUserReadQueryHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<UserStoreDto> Handle(UserStoreOfUserReadQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserStore user = await _userStoreRep.GetStoreOfUser(request.userId);
        return _mapper.Map<UserStoreDto>(user);
    }
}