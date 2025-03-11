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
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.User.Handlers.Commands
{
    public class UserReadCommandHandler : IRequestHandler<UserReadCommandReq, UserDto>
    {

        private IUserRepository _userRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public UserReadCommandHandler(IUserRepository userRep, IMapper mapper, IMediator madiiator)
        {
            _userRep = userRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }


        public async Task<UserDto> Handle(UserReadCommandReq request, CancellationToken cancellationToken)
        {
            var user = await _userRep.GetAsync(request.id);
            return _mapper.Map<UserDto>(user);
        }
    }
}
