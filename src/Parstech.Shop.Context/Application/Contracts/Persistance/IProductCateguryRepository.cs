using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

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