using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public interface ISelectionsAdminGrpcClient
{
    Task<List<SectionDto>> GetDiscountSectionsSelectAsync();
    Task<List<ProductSelectDto>> GetProductsSelectAsync();
    Task<List<CategurySelectDto>> GetCategoriesSelectAsync();
}