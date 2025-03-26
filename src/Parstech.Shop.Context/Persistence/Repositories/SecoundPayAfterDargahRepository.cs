using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class SecoundPayAfterDargahRepository:GenericRepository<SecoundPayAfterDargah>,ISecoundPayAfterDargahRepository
{
    private readonly DatabaseContext _context;
    public SecoundPayAfterDargahRepository(DatabaseContext context):base(context) 
    {
        _context=context;
    }

    public async Task<SecoundPayAfterDargah> GetByOrderId(int orderId)
    {
        return await _context.SecoundPayAfterDargahs.FirstOrDefaultAsync(u=>u.OrderId==orderId);
    }
}