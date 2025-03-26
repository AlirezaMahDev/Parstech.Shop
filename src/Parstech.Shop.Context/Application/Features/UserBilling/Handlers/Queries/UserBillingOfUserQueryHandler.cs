using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Handlers.Queries;

public class UserBillingOfUserQueryHandler : IRequestHandler<UserBillingOfUserQueryReq, UserBillingDto>
{
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public UserBillingOfUserQueryHandler(IUserBillingRepository userBillingRep, IMapper mapper)
    {
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }
    public async Task<UserBillingDto> Handle(UserBillingOfUserQueryReq request, CancellationToken cancellationToken)
    {
        var userBilling = await _userBillingRep.GetUserBillingByUserId(request.userId);
        return _mapper.Map<UserBillingDto>(userBilling);
    }
}