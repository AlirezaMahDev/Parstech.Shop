using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductRelatedRepository : IGenericRepository<ProductRelated>
{
    Task<List<ProductRelated>> GetRelatedProductsByProductId(int productId);
}