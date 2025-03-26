using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class WalletTypesRepository : GenericRepository<WalletType>, IWalletTypesRepository
{
    private readonly DatabaseContext _context;
    public WalletTypesRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}