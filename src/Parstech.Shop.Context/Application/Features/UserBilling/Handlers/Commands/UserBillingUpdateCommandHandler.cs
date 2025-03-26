using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Handlers.Commands;

public class UserBillingUpdateCommandHandler : IRequestHandler<UserBillingUpdateCommandReq, UserBillingDto>
{
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;

    public UserBillingUpdateCommandHandler(IUserBillingRepository userBillingRep, IMapper mapper, IUserRepository userRep)
    {
        _userBillingRep = userBillingRep;
        _userRep = userRep;
        _mapper = mapper;
    }

    public async Task<UserBillingDto> Handle(UserBillingUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var userBilling = _mapper.Map<Domain.Models.UserBilling>(request.userBillingDto);
           
        var result= await _userBillingRep.UpdateAsync(userBilling);
        return _mapper.Map<UserBillingDto>(result);

    }
}