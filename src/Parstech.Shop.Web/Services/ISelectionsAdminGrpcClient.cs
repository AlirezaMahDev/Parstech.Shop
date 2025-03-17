using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface ISelectionsAdminGrpcClient
{
    Task<List<SectionDto>> GetDiscountSectionsSelectAsync();
    Task<List<ProductSelectDto>> GetProductsSelectAsync();
    Task<List<CategurySelectDto>> GetCategoriesSelectAsync();
}