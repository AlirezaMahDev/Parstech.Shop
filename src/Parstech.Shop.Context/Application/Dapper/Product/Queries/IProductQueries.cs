namespace Parstech.Shop.Context.Application.Dapper.Product.Queries;

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