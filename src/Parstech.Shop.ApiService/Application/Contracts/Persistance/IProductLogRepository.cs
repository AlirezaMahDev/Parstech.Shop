using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductLog;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductLogRepository:IGenericRepository<ProductLog>
    {
        Task<List<ProductLog>> GetPriceProductLogWithProductId(int productId);
        Task<List<ProductLog>> GetSaleProductLogWithProductId(int productId);
        Task<List<ProductLog>> GetDiscountProductLogWithProductId(int productId);
        Task<List<ProductLog>> GetBaseProductLogWithProductId(int productId);
        Task<List<ProductLog>> GetProductLogWithProductId(int productId);

    }
}
