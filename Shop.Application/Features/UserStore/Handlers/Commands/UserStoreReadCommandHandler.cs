using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class UserStoreReadCommandHandler : IRequestHandler<UserStoreReadCommandReq, UserStoreDto>
    {

        private IUserStoreRepository _userStoreRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public UserStoreReadCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
        {
            _userStoreRep   = userStoreRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }


        public async Task<UserStoreDto> Handle(UserStoreReadCommandReq request, CancellationToken cancellationToken)
        {
            var item = await _userStoreRep.GetAsync(request.id);
            return _mapper.Map<UserStoreDto>(item);
        }
    }
    public class UserStoreReadsCommandHandler : IRequestHandler<UserStoreReadsCommandReq, List<UserStoreDto>>
    {

        private IUserStoreRepository _userStoreRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public UserStoreReadsCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
        {
            _userStoreRep = userStoreRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }


        public async Task<List<UserStoreDto>> Handle(UserStoreReadsCommandReq request, CancellationToken cancellationToken)
        {
            var list = await _userStoreRep.GetAll();
            return _mapper.Map<List<UserStoreDto>>(list);
        }
    }
}
