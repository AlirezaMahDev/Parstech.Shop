using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class UserReadByUserNameQueryHandler : IRequestHandler<UserReadByUserNameQueryReq, UserDto>
    {
        private readonly IUserRepository _userRep;
        private readonly IMapper _mapper;

        public UserReadByUserNameQueryHandler(IUserRepository userRep, IMapper mapper)
        {
            _userRep = userRep;
            _mapper = mapper;
        }
        public async Task<UserDto> Handle(UserReadByUserNameQueryReq request, CancellationToken cancellationToken)
        {
            var user =await _userRep.GetUserByUserName(request.userName);
            return _mapper.Map<UserDto>(user);
        }
    }
}
