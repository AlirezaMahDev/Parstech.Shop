using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class WalletTypesRepository : GenericRepository<WalletType>, IWalletTypesRepository
{
    private readonly DatabaseContext _context;

    public WalletTypesRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}