using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.UserStore.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Application.Features.UserStore.Handlers.Queries
{
    public class UserStoreOfUserReadQueryHandler : IRequestHandler<UserStoreOfUserReadQueryReq, UserStoreDto>
    {
        private IUserStoreRepository _userStoreRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public UserStoreOfUserReadQueryHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
        {
            _userStoreRep = userStoreRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<UserStoreDto> Handle(UserStoreOfUserReadQueryReq request, CancellationToken cancellationToken)
        {
            var user = await _userStoreRep.GetStoreOfUser(request.userId);
            return _mapper.Map<UserStoreDto>(user);
        }
    }
}
