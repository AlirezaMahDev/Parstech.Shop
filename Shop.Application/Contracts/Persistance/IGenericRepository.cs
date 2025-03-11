using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
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
}
