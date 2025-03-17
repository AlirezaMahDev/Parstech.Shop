using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Handlers.Queries;

public class UserShippingOfUserQueryHandler : IRequestHandler<UserShippingOfUserQueryReq, List<UserShippingDto>>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingOfUserQueryHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }


    public async Task<List<UserShippingDto>> Handle(UserShippingOfUserQueryReq request,
        CancellationToken cancellationToken)
    {
        List<UserShippingDto> userShippingList = await _userShippingRep.GetShippingOfUser(request.userId);
        return _mapper.Map<List<UserShippingDto>>(userShippingList);
    }
}