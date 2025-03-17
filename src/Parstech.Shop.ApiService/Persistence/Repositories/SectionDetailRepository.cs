using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class SectionDetailRepository : GenericRepository<SectionDetail>, ISectionDetailRepository
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