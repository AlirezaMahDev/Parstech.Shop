using Shop.Application.DTOs.ProductStockPrice;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IProductStockPriceRepository:IGenericRepository<ProductStockPrice>
    {
        Task<int> GetFirstProductStockPriceIdFromProductId(int productId);
        Task<List<ProductStockPrice>> GetAllByProductId(int productId);
        Task<ProductStockPrice?> DapperGetProductStockPriceById(int id);
        Task<ProductStockPrice?> GetProductStockByProductIdAndStoreId(int productId, int storeId);
        Task<bool> ExistStockForProductIdAndStore(int productId, int storeId);
        Task<bool> ExistStockForParentProduct(int productId);
        //Task<bool> ExistOrder(int skip, int take);


        Task<List<ProductStockPriceDto>> GetAllStock(int productId, bool UserCateguryExist);
        Task<int?> GetBestStockId(int productId);
        Task<int?> GetBestStockUserCateguryId(int productId);
    }
}
