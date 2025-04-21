using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.IRole.Requests.Commands;

namespace Shop.Application.Features.IRole.Handlers.Commands
{
    public class IRoleDeleteCommandHandler : IRequestHandler<IRoleDeleteCommandReq, Unit>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRep;

        public IRoleDeleteCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRoleRepository roleRep)
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
}
