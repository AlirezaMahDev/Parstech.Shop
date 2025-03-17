using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductRelatedRepository : IGenericRepository<ProductRelated>
{
    Task<List<ProductRelated>> GetRelatedProductsByProductId(int productId);
}