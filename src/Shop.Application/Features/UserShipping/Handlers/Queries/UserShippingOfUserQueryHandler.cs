using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserShipping;
using Shop.Application.Features.UserShipping.Requests.Queries;

namespace Shop.Application.Features.UserShipping.Handlers.Queries
{
    public class UserShippingOfUserQueryHandler : IRequestHandler<UserShippingOfUserQueryReq, List<UserShippingDto>>
    {
        private readonly IUserShippingRepository _userShippingRep;
        private readonly IMapper _mapper;

        public UserShippingOfUserQueryHandler(IUserShippingRepository userShippingRep, IMapper mapper)
        {
            _userShippingRep = userShippingRep;
            _mapper = mapper;
        }


        public async Task<List<UserShippingDto>> Handle(UserShippingOfUserQueryReq request, CancellationToken cancellationToken)
        {
            var userShippingList = await _userShippingRep.GetShippingOfUser(request.userId);
            return _mapper.Map<List<UserShippingDto>>(userShippingList);
        }
    }
}
