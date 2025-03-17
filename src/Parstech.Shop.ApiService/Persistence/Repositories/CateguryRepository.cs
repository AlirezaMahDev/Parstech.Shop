using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class CateguryRepository : GenericRepository<Categury>, ICateguryRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public CateguryRepository(DatabaseContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Categury>> GetCateguryByParentId(int parentId, int? Row)
    {
        if (parentId == 0)
        {
            return await _context.Categuries.Where(u => u.ParentId == null).ToListAsync();
        }
        else
        {
            if (Row != null)
            {
                return await _context.Categuries.Where(u => u.ParentId == parentId && u.Row == Row).ToListAsync();
            }
            else
            {
                return await _context.Categuries.Where(u => u.ParentId == parentId).ToListAsync();
            }
        }
    }


    public async Task<List<Categury>> GetAllParentCategury()
    {
        return await _context.Categuries.Where(u => u.IsParnet).ToListAsync();
    }


    public async Task<List<Categury>> GetShowCateguryByParentId(int parentId)
    {
        if (parentId == 0)
        {
            return await _context.Categuries.Where(u => u.Show && u.ParentId == null).ToListAsync();
        }
        else
        {
            return await _context.Categuries.Where(u => u.Show && u.ParentId == parentId).ToListAsync();
        }
    }

    public async Task<Categury?> GetCateguryByLatinName(string latinName)
    {
        return await _context.Categuries.FirstOrDefaultAsync(u => u.LatinGroupTitle == latinName);
    }

    public async Task<Categury?> GetCateguryByName(string name)
    {
        return await _context.Categuries.FirstOrDefaultAsync(u => u.GroupTitle == name);
    }
}