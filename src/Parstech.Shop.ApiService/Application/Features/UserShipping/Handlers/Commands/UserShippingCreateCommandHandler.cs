using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Handlers.Commands;

public class UserShippingCreateCommandHandler : IRequestHandler<UserShippingCreateCommandReq, UserShippingDto>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingCreateCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }

    public async Task<UserShippingDto> Handle(UserShippingCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserShipping? userShipping = _mapper.Map<Shared.Models.UserShipping>(request.UserShippingDto);
        Shared.Models.UserShipping result = await _userShippingRep.AddAsync(userShipping);
        return _mapper.Map<UserShippingDto>(result);
    }
}