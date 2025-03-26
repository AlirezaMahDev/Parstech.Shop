using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Handlers.Commands;

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
        var userShipping = _mapper.Map<Domain.Models.UserShipping>(request.UserShippingDto);
        var result= await _userShippingRep.AddAsync(userShipping);
        return _mapper.Map<UserShippingDto>(result);

    }
}