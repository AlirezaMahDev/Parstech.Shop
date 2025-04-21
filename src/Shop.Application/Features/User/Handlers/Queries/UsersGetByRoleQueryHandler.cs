using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class UsersGetByRoleQueryHandler : IRequestHandler<UsersGetByRoleQueryReq, List<UserDto>>
    {
        private readonly IUserRepository _userRep;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UsersGetByRoleQueryHandler(IUserRepository userRep,IMapper mapper,UserManager<IdentityUser> userManager)
        {
            _userManager= userManager;
            _userRep = userRep;
            _mapper=mapper;
        }
        public async Task<List<UserDto>> Handle(UsersGetByRoleQueryReq request, CancellationToken cancellationToken)
        {
            var result=new List<UserDto>();
           var list=await _userManager.GetUsersInRoleAsync(request.role);
            foreach(var item in list)
            {
                var user=await _userRep.GetUserByUserName(item.UserName);
                result.Add(_mapper.Map<UserDto>(user));
            }
            return result;
        }
    }
}
