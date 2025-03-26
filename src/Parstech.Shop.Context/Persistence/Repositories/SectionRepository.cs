using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class SectionRepository:GenericRepository<Section>,ISectionRepository
{
    private readonly DatabaseContext _context;

    public SectionRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> CheckSectionDetailExist(int SectionId)
    {
        if (await _context.SectionDetails.AnyAsync(u=>u.SectionId==SectionId))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<Section> GetByOlaviat(int Olaviat)
    {
        return await _context.Sections.FirstOrDefaultAsync(u => u.Sort == Olaviat);
    }
    public async Task<Section> GetByStore(int storeId)
    {
        return await _context.Sections.FirstOrDefaultAsync(u => u.StoreId == storeId);
    }
    public async Task<List<Section>> GetSectionsOfStore(int? storeId)
    {
        return await _context.Sections.Where(u => u.StoreId == storeId).ToListAsync();
    }
        
}