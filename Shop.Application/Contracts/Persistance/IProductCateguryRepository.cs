using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.ProductCategury;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductCateguryRepository:IGenericRepository<ProductCategury>
    {
        Task<ProductCategury?> GetCateguryByProduct(int productId);
        Task<List<ProductCategury>> GetCateguriesByProduct(int productId);
        Task<bool> ExistProductCategury(ProductCateguryDto productCategury);
        Task<List<ProductCategury>> GetProductCateguriesByCateguryId(int categuryId);
        Task<bool> ProductHaveCategury(int productId, int categuryId);
        Task<bool> ExistProductCateguryForCateguryId(int categuryId);
        Task<ProductCategury> GetProductCateguryByProductIdAndCateguryName(int productId, string categuryName);
    }
}
