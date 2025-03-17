using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Handlers.Commands;

public class IRoleCreateCommandHandler : IRequestHandler<IRoleCreateCommandReq, Irole>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRoleRepository _roleRep;

    public IRoleCreateCommandHandler(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IRoleRepository roleRep)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _roleRep = roleRep;
    }


    public async Task<Irole> Handle(IRoleCreateCommandReq request, CancellationToken cancellationToken)
    {
        IdentityRole role = new();
        role.Name = request.roleDto.Name;
        await _roleManager.CreateAsync(role);
        Irole irole = await _roleRep.GetIdentityRole(role.Id);
        irole.PersianName = request.roleDto.PersianName;
        await _roleRep.UpdateAsync(irole);
        return irole;
    }
}