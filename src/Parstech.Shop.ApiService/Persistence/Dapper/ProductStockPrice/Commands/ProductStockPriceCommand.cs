using Parstech.Shop.ApiService.Application.Dapper.ProductStockPrice.Commands;

namespace Parstech.Shop.ApiService.Persistence.Dapper.ProductStockPrice.Commands;

public class ProductStockPriceCommand : IProductStockPriceCommand
{
    public string GetProductStockPriceById => "Select * From ProductStockPrice Where Id=@Id";
}