﻿using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Handlers.Queries;

public class UserShippingOfUserQueryHandler : IRequestHandler<UserShippingOfUserQueryReq, List<UserShippingDto>>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingOfUserQueryHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }


    public async Task<List<UserShippingDto>> Handle(UserShippingOfUserQueryReq request, CancellationToken cancellationToken)
    {
        var userShippingList = await _userShippingRep.GetShippingOfUser(request.userId);
        return _mapper.Map<List<UserShippingDto>>(userShippingList);
    }
}