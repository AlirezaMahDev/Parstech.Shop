using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Handlers.Commands;

public class IRoleDeleteCommandHandler : IRequestHandler<IRoleDeleteCommandReq, Unit>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IRoleRepository _roleRep;

    public IRoleDeleteCommandHandler(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IRoleRepository roleRep)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _roleRep = roleRep;
    }

    public async Task<Unit> Handle(IRoleDeleteCommandReq request, CancellationToken cancellationToken)
    {
        await _roleRep.DeleteIdentityRole(request.id);
        return Unit.Value;
    }
}