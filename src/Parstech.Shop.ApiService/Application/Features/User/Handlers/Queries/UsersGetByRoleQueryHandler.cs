using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class UsersGetByRoleQueryHandler : IRequestHandler<UsersGetByRoleQueryReq, List<UserDto>>
{
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersGetByRoleQueryHandler(IUserRepository userRep, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _userRep = userRep;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(UsersGetByRoleQueryReq request, CancellationToken cancellationToken)
    {
        List<UserDto>? result = new();
        IList<IdentityUser> list = await _userManager.GetUsersInRoleAsync(request.role);
        foreach (IdentityUser item in list)
        {
            Domain.Models.User? user = await _userRep.GetUserByUserName(item.UserName);
            result.Add(_mapper.Map<UserDto>(user));
        }

        return result;
    }
}