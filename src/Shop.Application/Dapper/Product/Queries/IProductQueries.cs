using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dapper.Product.Queries
{
    public interface IProductQueries
    {

        string GetListPagingByGroup { get; }
        string GetListByGroup { get; }
        string GetLastListByGroup { get; }
        string GetLastListDiscountByGroup { get; }
        string GetMainImage { get; }
        string GetAllStoreForProduct { get; }
        string GetFirstVariation { get; }
        string GetOneProduct { get; }
        string GetListPagingByRep { get; }
        string GetListPagingByStore { get; }
        string GetListByRep { get; }
        string GetListByStore { get; }
        string GetAllListPaging { get; }
        string GetAllList { get; }
        string GetListVariationByParentId { get; }
        string GetProductStockPriceById { get; }
        string GetOneProductFull { get; }
        string GetProductsPagingForAdmin { get; }
        string GetProductForAdmin { get; }
        string GetChildsForAdmin { get; }
        string GetProductStocksByProductIdForAdmin { get; }
        string GetProductStockPriceByRepId { get; }

    }
}
