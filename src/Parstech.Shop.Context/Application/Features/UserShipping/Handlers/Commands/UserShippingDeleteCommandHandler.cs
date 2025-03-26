using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Handlers.Commands;

public class UserShippingDeleteCommandHandler : IRequestHandler<UserShippingDeleteCommandReq, Unit>
{
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;

    public UserShippingDeleteCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
    {
        _userShippingRep = userShippingRep;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UserShippingDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var userShipping = await _userShippingRep.GetAsync(request.id);
        await _userShippingRep.DeleteAsync(userShipping);
        return Unit.Value;
    }
}