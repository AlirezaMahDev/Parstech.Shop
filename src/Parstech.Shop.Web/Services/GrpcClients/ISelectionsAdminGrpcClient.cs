using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Section;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface ISelectionsAdminGrpcClient
    {
        Task<List<SectionDto>> GetDiscountSectionsSelectAsync();
        Task<List<ProductSelectDto>> GetProductsSelectAsync();
        Task<List<CategurySelectDto>> GetCategoriesSelectAsync();
    }
} 