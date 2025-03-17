using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Handlers.Commands;

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
        Shared.Models.UserBilling? userBilling = await _userBillingRep.GetAsync(request.id);
        return _mapper.Map<UserBillingDto>(userBilling);
    }
}