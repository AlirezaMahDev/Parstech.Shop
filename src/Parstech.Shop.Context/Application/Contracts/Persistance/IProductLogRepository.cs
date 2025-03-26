using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IProductLogRepository:IGenericRepository<ProductLog>
{
    Task<List<ProductLog>> GetPriceProductLogWithProductId(int productId);
    Task<List<ProductLog>> GetSaleProductLogWithProductId(int productId);
    Task<List<ProductLog>> GetDiscountProductLogWithProductId(int productId);
    Task<List<ProductLog>> GetBaseProductLogWithProductId(int productId);
    Task<List<ProductLog>> GetProductLogWithProductId(int productId);

}