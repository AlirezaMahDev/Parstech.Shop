using Parstech.Shop.Context.Application.Dapper.Product.Commands;

namespace Parstech.Shop.Context.Persistence.Dapper.Product.Commands;

public class ProductCommand: IProductCommand
{

    public string GetProductById => "Select * From Product Where Id=@Id";
}