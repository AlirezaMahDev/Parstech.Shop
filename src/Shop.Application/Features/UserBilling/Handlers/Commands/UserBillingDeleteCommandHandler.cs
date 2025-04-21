using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.Features.UserBilling.Requests.Commands;

namespace Shop.Application.Features.UserBilling.Handlers.Commands
{
    public class UserBillingDeleteCommandHandler : IRequestHandler<UserBillingDeleteCommandReq, Unit>
    {
        private readonly IUserBillingRepository _userBillingRep;
        private readonly IMapper _mapper;

        public UserBillingDeleteCommandHandler(IUserBillingRepository userBillingRep, IMapper mapper)
        {
            _userBillingRep = userBillingRep;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UserBillingDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var userBilling =await _userBillingRep.GetAsync(request.id);
            await _userBillingRep.DeleteAsync(userBilling);
            return Unit.Value;
        }
    }
}
