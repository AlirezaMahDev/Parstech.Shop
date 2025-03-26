using Microsoft.EntityFrameworkCore;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class SectionDetailRepository:GenericRepository<SectionDetail>,ISectionDetailRepository
{
    private readonly DatabaseContext _context;

    public SectionDetailRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<SectionDetail>> GetDetailsOfSection(int SectionId)
    {
        return await _context.SectionDetails.Where(u => u.SectionId == SectionId).ToListAsync();
    }
}