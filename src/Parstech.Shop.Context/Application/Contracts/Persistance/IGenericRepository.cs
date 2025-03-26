namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IGenericRepository<TEntity> : IDisposable
{
    Task<TEntity?> GetAsync(int id);
    Task<TEntity?> GetAsync(string id);
        
    Task<IReadOnlyList<TEntity>> GetAll();
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<int> GetCountOfTable(TEntity entity);



}