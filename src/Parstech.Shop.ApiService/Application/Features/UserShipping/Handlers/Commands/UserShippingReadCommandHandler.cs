using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Handlers.Commands;

public class UserShippingReadCommandHandler : IRequestHandler<UserShippingReadCommandReq, UserShippingDto>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingReadCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }

    public async Task<UserShippingDto> Handle(UserShippingReadCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserShipping? userShipping = await _userShippingRep.GetAsync(request.id);
        return _mapper.Map<UserShippingDto>(userShipping);
    }
}