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
            var userBilling = _mapper.Map<Domain.Models.UserBilling>(request.userBillingDto);
           var result= await _userBillingRep.AddAsync(userBilling);
           return _mapper.Map<UserBillingDto>(result);

        }
    }
}
