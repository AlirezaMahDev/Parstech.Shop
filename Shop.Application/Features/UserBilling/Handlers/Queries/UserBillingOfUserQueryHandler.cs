using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.Features.UserBilling.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserBilling.Handlers.Queries
{
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
}
