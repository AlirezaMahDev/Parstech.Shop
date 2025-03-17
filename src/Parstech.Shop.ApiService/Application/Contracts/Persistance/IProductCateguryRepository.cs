using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductCateguryRepository : IGenericRepository<ProductCategury>
{
    Task<ProductCategury?> GetCateguryByProduct(int productId);
    Task<List<ProductCategury>> GetCateguriesByProduct(int productId);
    Task<bool> ExistProductCategury(ProductCateguryDto productCategury);
    Task<List<ProductCategury>> GetProductCateguriesByCateguryId(int categuryId);
    Task<bool> ProductHaveCategury(int productId, int categuryId);
    Task<bool> ExistProductCateguryForCateguryId(int categuryId);
    Task<ProductCategury> GetProductCateguryByProductIdAndCateguryName(int productId, string categuryName);
}