using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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
        List<UserShipping> shippings = await _context.UserShippings.Where(u => u.UserId == userId).ToListAsync();
        return _mapper.Map<List<UserShippingDto>>(shippings);
    }
}