using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class SecoundPayAfterDargahRepository : GenericRepository<SecoundPayAfterDargah>,
    ISecoundPayAfterDargahRepository
{
    private readonly DatabaseContext _context;

    public SecoundPayAfterDargahRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SecoundPayAfterDargah> GetByOrderId(int orderId)
    {
        return await _context.SecoundPayAfterDargahs.FirstOrDefaultAsync(u => u.OrderId == orderId);
    }
}