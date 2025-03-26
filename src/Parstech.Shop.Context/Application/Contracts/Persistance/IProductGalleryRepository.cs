using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IProductGallleryRepository:IGenericRepository<ProductGallery>
{
    Task<ProductGallery?> GetMainImageOfProduct(int productId);
    Task<ProductGallery?> GetGalleryByProduct(int productId);
    Task<List<ProductGallery>> GetGalleriesByProduct(int productId);
        
}