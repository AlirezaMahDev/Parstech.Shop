using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Handlers.Commands;

public class UserBillingUpdateCommandHandler : IRequestHandler<UserBillingUpdateCommandReq, UserBillingDto>
{
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;

    public UserBillingUpdateCommandHandler(IUserBillingRepository userBillingRep,
        IMapper mapper,
        IUserRepository userRep)
    {
        _userBillingRep = userBillingRep;
        _userRep = userRep;
        _mapper = mapper;
    }

    public async Task<UserBillingDto> Handle(UserBillingUpdateCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.UserBilling? userBilling = _mapper.Map<Domain.Models.UserBilling>(request.userBillingDto);

        Domain.Models.UserBilling? result = await _userBillingRep.UpdateAsync(userBilling);
        return _mapper.Map<UserBillingDto>(result);
    }
}