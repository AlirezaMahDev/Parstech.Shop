using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

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
        Domain.Models.UserBilling? userBilling = _mapper.Map<Domain.Models.UserBilling>(request.userBillingDto);
        Domain.Models.UserBilling? result = await _userBillingRep.AddAsync(userBilling);
        return _mapper.Map<UserBillingDto>(result);
    }
}