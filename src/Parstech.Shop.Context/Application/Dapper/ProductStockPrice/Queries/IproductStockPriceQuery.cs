namespace Parstech.Shop.Context.Application.Dapper.ProductStockPrice.Queries;

public interface IproductStockPriceQuery
{
    string GetDiscountProductSTockPricePaging(int skip);
}