using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class PayTypeRepository:GenericRepository<PayType>, IPayTypeRepository
{
    private readonly DatabaseContext _context;
    public PayTypeRepository(DatabaseContext context):base(context)
    {
        _context = context;
    }

    public async Task<List<PayType>> GetActiveList()
    {
        return _context.PayTypes.Where(x => x.IsActive).ToList();
    }
}