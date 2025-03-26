using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IProductStockPriceSectionRepository:IGenericRepository<ProductStockPriceSection>
{
    Task<List<ProductStockPriceSection>> GetSectionOfProductStockPrice(int id);
}