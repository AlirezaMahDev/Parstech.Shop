using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DatabaseContext _context;
    private readonly IdentityContext _icontext;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public UserRepository(DatabaseContext context,
        IdentityContext icontext,
        IUserBillingRepository userBillingRep,
        IMapper mapper) : base(context)
    {
        _context = context;
        _icontext = icontext;
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }


    public async Task<List<IUserRoleDto>> GetUserRoles(string UserId)
    {
        List<IUserRoleDto> result = new();
        List<IdentityUserRole<string>> ur = await _icontext.UserRoles.Where(u => u.UserId == UserId).ToListAsync();
        foreach (IdentityUserRole<string> userRole in ur)
        {
            Irole? role = await _context.Iroles.FindAsync(userRole.RoleId);
            IdentityUser? user = await _icontext.Users.FindAsync(UserId);
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
        if (password == confirmPassword)
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
        IdentityUser? item = await _icontext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return item;
    }

    public async Task<bool> ExistUserCategury(string userName)
    {
        User? user = await GetUserByUserName(userName);
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
        CateguryOfUser? Usercat = await _context.CateguryOfUsers.FirstOrDefaultAsync(u => u.Id == id);
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