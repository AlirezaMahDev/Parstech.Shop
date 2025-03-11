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
using Shop.Application.Features.UserShipping.Requests.Commands;

namespace Shop.Application.Features.UserShipping.Handlers.Commands
{
    public class UserShippingDeleteCommandHandler : IRequestHandler<UserShippingDeleteCommandReq, Unit>
    {
        private readonly IUserShippingRepository _userShippingRep;
        private readonly IMapper _mapper;

        public UserShippingDeleteCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
        {
            _userShippingRep = userShippingRep;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UserShippingDeleteCommandReq request, CancellationToken cancellationToken)
        {
            var userShipping = await _userShippingRep.GetAsync(request.id);
            await _userShippingRep.DeleteAsync(userShipping);
            return Unit.Value;
        }
    }
}
