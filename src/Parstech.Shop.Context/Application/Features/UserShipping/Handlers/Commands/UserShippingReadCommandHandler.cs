using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Handlers.Commands;

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
        var userShipping = await _userShippingRep.GetAsync(request.id);
        return _mapper.Map<UserShippingDto>(userShipping);
    }
}