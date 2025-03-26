using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.IUserRole;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DatabaseContext _context;
    private readonly IdentityContext _icontext;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public UserRepository(DatabaseContext context, IdentityContext icontext, IUserBillingRepository userBillingRep, IMapper mapper) : base(context)
    {
        _context = context;
        _icontext = icontext;
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }


    public async Task<List<IUserRoleDto>> GetUserRoles(string UserId)
    {
        List<IUserRoleDto> result = new();
        var ur =await _icontext.UserRoles.Where(u => u.UserId == UserId).ToListAsync();
        foreach (var userRole in ur)
        {
            var role =await _context.Iroles.FindAsync(userRole.RoleId);
            var user =await _icontext.Users.FindAsync(UserId);
            IUserRoleDto urDto = new()
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                RoleName = role.Name,
                PersianRoleName = role.PersianName,
                UserName = user.UserName
            };
            result.Add(urDto);
        }

        return result;
    }

    public bool PaswordsValid(string password, string confirmPassword)
    {
        if (password==confirmPassword)
        {
            return true;
        }
        return false;
    }
    public int GetCountOfUsers()
    {
            
        return _context.Users.Count();
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<IdentityUser> GetIUserByUserName(string userName)
    {
        var item =await _icontext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return item;
    }

    public async Task<bool> ExistUserCategury(string userName)
    {
        var user =await GetUserByUserName(userName);
        if (await _context.UserCateguries.AnyAsync(u => u.UserId == user.Id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<string?> GetUserCategury(int id)
    {
        var Usercat =await _context.CateguryOfUsers.FirstOrDefaultAsync(u => u.Id == id);
        if (Usercat != null)
        {
            return Usercat.CateguryName;
        }
        else
        {
            return null;
        }
    }
}