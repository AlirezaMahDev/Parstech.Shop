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
using Shop.Domain.Models;

namespace Shop.Application.Features.UserShipping.Handlers.Commands
{
    public class UserShippingUpdateCommandHandler : IRequestHandler<UserShippingUpdateCommandReq, UserShippingDto>
    {
        private readonly IUserShippingRepository _userShippingRep;
        private readonly IMapper _mapper;

        public UserShippingUpdateCommandHandler(IUserShippingRepository userShippingRep, IMapper mapper)
        {
            _userShippingRep = userShippingRep;
            _mapper = mapper;
        }

        public async Task<UserShippingDto> Handle(UserShippingUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var userShipping = _mapper.Map<Shop.Domain.Models.UserShipping>(request.UserShippingDto);
           var result= await _userShippingRep.UpdateAsync(userShipping);
           return _mapper.Map<UserShippingDto>(result);

        }
    }
}
