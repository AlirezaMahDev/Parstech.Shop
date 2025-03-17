using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class StateRepository : GenericRepository<State>, IStateRepository
{
    private readonly DatabaseContext _context;

    public StateRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}