using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Shop.Application.Features.UserStore.Handlers.Commands
{
    public class UserStoreCreateCommandHandler : IRequestHandler<UserStoreCreateCommandReq, UserStoreDto>
    {
        private IUserStoreRepository _userStoreRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public UserStoreCreateCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
        {
            _userStoreRep = userStoreRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }
        public async Task<UserStoreDto> Handle(UserStoreCreateCommandReq request, CancellationToken cancellationToken)
        {
            var userStore = _mapper.Map<Domain.Models.UserStore>(request.userStoreDto);
            
            var userResult=await _userStoreRep.AddAsync(userStore);
            return _mapper.Map<UserStoreDto>(userResult);
        }
    }
}
