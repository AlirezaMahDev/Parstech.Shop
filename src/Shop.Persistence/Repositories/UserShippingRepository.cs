using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserShipping;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class UserShippingRepository : GenericRepository<UserShipping>, IUserShippingRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserShippingRepository(DatabaseContext context,
            IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserShipping> GetFirstShippingOfUser(int userId)
        {
            UserShipping userShipping = new UserShipping();
            
            userShipping = await _context.UserShippings.FirstOrDefaultAsync(u => u.UserId == userId);
            return userShipping;
        }

        public async Task<List<UserShippingDto>> GetShippingOfUser(int userId)
        {
            var shippings = await _context.UserShippings.Where(u => u.UserId == userId).ToListAsync();
            return _mapper.Map<List<UserShippingDto>>(shippings);
        }

    }
}
