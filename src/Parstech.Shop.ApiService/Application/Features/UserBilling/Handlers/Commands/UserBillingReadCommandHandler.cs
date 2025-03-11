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
}
