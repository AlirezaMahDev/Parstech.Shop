using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.IRole.Requests.Commands;
using Shop.Domain.Models;

namespace Shop.Application.Features.IRole.Handlers.Commands
{
    public class IRoleCreateCommandHandler : IRequestHandler<IRoleCreateCommandReq, Irole>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRep;

        public IRoleCreateCommandHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IRoleRepository roleRep)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRep = roleRep;
        }


        public async Task<Irole> Handle(IRoleCreateCommandReq request, CancellationToken cancellationToken)
        {
            var role = new IdentityRole();
            role.Name = request.roleDto.Name;
            await _roleManager.CreateAsync(role);
            var irole =await _roleRep.GetIdentityRole(role.Id);
            irole.PersianName = request.roleDto.PersianName;
            await _roleRep.UpdateAsync(irole);
            return irole;
        }
    }
}
