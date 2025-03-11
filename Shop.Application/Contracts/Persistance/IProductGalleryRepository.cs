using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductGallleryRepository:IGenericRepository<ProductGallery>
    {
        Task<ProductGallery?> GetMainImageOfProduct(int productId);
        Task<ProductGallery?> GetGalleryByProduct(int productId);
        Task<List<ProductGallery>> GetGalleriesByProduct(int productId);
        
    }
}
