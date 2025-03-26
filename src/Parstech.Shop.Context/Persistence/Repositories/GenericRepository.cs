using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{

    #region Constractor
    private DatabaseContext _context;

    public GenericRepository(DatabaseContext context)
    {
        _context = context;
    }

    #endregion


    public async Task<TEntity?> GetAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    } 
        
    public async Task<TEntity?> GetAsync(string id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }


    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<int> GetCountOfTable(TEntity entity)
    {
        var all=await _context.Set<TEntity>().ToListAsync();
        return all.Count;
    }
        


}