using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IProductRelatedRepository:IGenericRepository<ProductRelated>
{
    Task<List<ProductRelated>>GetRelatedProductsByProductId(int productId);
}