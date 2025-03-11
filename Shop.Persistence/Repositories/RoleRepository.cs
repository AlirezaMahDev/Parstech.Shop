using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class RoleRepository:GenericRepository<Irole>,IRoleRepository
    {
        private readonly DatabaseContext _context;
        //private readonly IdentityContext _icontext;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IRoleRepository _roleRep;

        public RoleRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }


        public async Task DeleteIdentityRole(string id)
        {
            //var role =await _icontext.Roles.FindAsync(id);
            //await _roleManager.DeleteAsync(role);
        }

        public async Task<Irole> GetIdentityRole(string id)
        {
            return await _context.Iroles.FindAsync(id);
        }
    }
}
