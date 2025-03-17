using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class PayTypeRepository : GenericRepository<PayType>, IPayTypeRepository
{
    private readonly DatabaseContext _context;

    public PayTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<PayType>> GetActiveList()
    {
        return _context.PayTypes.Where(x => x.IsActive).ToList();
    }
}