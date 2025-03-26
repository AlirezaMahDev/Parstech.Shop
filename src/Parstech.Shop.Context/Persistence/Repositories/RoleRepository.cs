using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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