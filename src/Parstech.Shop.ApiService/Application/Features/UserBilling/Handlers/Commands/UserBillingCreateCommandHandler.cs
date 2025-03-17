using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Handlers.Commands;

public class UserBillingCreateCommandHandler : IRequestHandler<UserBillingCreateCommandReq, UserBillingDto>
{
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public UserBillingCreateCommandHandler(IUserBillingRepository userBillingRep, IMapper mapper)
    {
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }

    public async Task<UserBillingDto> Handle(UserBillingCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserBilling? userBilling = _mapper.Map<Shared.Models.UserBilling>(request.userBillingDto);
        Shared.Models.UserBilling result = await _userBillingRep.AddAsync(userBilling);
        return _mapper.Map<UserBillingDto>(result);
    }
}