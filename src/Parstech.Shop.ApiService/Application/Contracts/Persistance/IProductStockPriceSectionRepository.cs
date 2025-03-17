using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductStockPriceSectionRepository : IGenericRepository<ProductStockPriceSection>
{
    Task<List<ProductStockPriceSection>> GetSectionOfProductStockPrice(int id);
}