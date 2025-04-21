using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.IUserRole;
using Shop.Application.DTOs.SocialSetting;
using Shop.Application.DTOs.User;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
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
            List<IUserRoleDto> result = new List<IUserRoleDto>();
            var ur =await _icontext.UserRoles.Where(u => u.UserId == UserId).ToListAsync();
            foreach (var userRole in ur)
            {
                var role =await _context.Iroles.FindAsync(userRole.RoleId);
                var user =await _icontext.Users.FindAsync(UserId);
                IUserRoleDto urDto = new IUserRoleDto()
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
}
