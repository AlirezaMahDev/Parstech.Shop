using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Handlers.Commands;

public class UserBillingReadCommandHandler : IRequestHandler<UserBillingReadCommandReq, UserBillingDto>
{
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public UserBillingReadCommandHandler(IUserBillingRepository userBillingRep, IMapper mapper)
    {
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }
    public async Task<UserBillingDto> Handle(UserBillingReadCommandReq request, CancellationToken cancellationToken)
    {
        var userBilling =await _userBillingRep.GetAsync(request.id);
        return _mapper.Map<UserBillingDto>(userBilling);
    }
}