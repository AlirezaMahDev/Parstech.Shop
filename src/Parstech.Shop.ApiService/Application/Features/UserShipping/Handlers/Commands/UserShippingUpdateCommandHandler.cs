using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Handlers.Commands;

public class UserShippingUpdateCommandHandler : IRequestHandler<UserShippingUpdateCommandReq, UserShippingDto>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingUpdateCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }

    public async Task<UserShippingDto> Handle(UserShippingUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var userShipping = _mapper.Map<Shop.Domain.Models.UserShipping>(request.UserShippingDto);
        Domain.Models.UserShipping? result = await _userShippingRep.UpdateAsync(userShipping);
        return _mapper.Map<UserShippingDto>(result);
    }
}