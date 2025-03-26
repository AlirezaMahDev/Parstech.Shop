using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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
        UserShipping userShipping = new();
            
        userShipping = await _context.UserShippings.FirstOrDefaultAsync(u => u.UserId == userId);
        return userShipping;
    }

    public async Task<List<UserShippingDto>> GetShippingOfUser(int userId)
    {
        var shippings = await _context.UserShippings.Where(u => u.UserId == userId).ToListAsync();
        return _mapper.Map<List<UserShippingDto>>(shippings);
    }

}