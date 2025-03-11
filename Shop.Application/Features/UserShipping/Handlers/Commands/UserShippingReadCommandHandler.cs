using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.DTOs.UserShipping;
using Shop.Application.Features.UserBilling.Requests.Commands;
using Shop.Application.Features.UserShipping.Requests.Commands;

namespace Shop.Application.Features.UserShipping.Handlers.Commands
{
    public class UserShippingReadCommandHandler : IRequestHandler<UserShippingReadCommandReq, UserShippingDto>
    {
        private readonly IUserShippingRepository _userShippingRep;
        private readonly IMapper _mapper;

        public UserShippingReadCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
        {
            _userShippingRep = userShippingRep;
            _mapper = mapper;
        }
        public async Task<UserShippingDto> Handle(UserShippingReadCommandReq request, CancellationToken cancellationToken)
        {
            var userShipping = await _userShippingRep.GetAsync(request.id);
            return _mapper.Map<UserShippingDto>(userShipping);
        }
    }
}
