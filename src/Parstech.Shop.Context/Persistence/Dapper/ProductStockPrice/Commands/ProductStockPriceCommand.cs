using Parstech.Shop.Context.Application.Dapper.ProductStockPrice.Commands;

namespace Parstech.Shop.Context.Persistence.Dapper.ProductStockPrice.Commands;

public class ProductStockPriceCommand : IProductStockPriceCommand
{
    public string GetProductStockPriceById => "Select * From ProductStockPrice Where Id=@Id";
}