using Parstech.Shop.ApiService.Application.Dapper.Product.Commands;

namespace Parstech.Shop.ApiService.Persistence.Dapper.Product.Commands;

public class ProductCommand : IProductCommand
{
    public string GetProductById => "Select * From Product Where Id=@Id";
}