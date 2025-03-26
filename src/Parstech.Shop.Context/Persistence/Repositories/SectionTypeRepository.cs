using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class SectionTypeRepository:GenericRepository<SectionType>,ISectionTypeRepository
{
    private readonly DatabaseContext _context;

    public SectionTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}