namespace Parstech.Shop.Context.Application.DTOs.ProductProperty;

public class ProductPropertyDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int PropertyId { get; set; }
    public string Caption { get; set; }
    public string Value { get; set; }


    public string PropertyName { get; set; }
    public int ProductId { get; set; }
}

public class BaseProductPropertyDto
{
    public int Id { get; set; }
    public string PropertyCategury { get; set; }
    public List<ProductPropertyDto> Properties { get; set; }

}

public class CompareDto
{
    public int userProductId { get; set; }
    public int productId { get; set; }
    public int productStockId { get; set; }
    public string name { get; set; }
    public string shortLink { get; set; }
    public string code { get; set; }
    public string image { get; set; }
    public List<ProductPropertyDto> commonProperties { get; set; }
    public List<ProductPropertyDto> productProperties { get; set; }
}

public class FavoriteDto
{
    public int userProductId { get; set; }
    public int productId { get; set; }
    public int productStockId { get; set; }
    public string name { get; set; }
    public string code { get; set; }
    public string shortLink { get; set; }
    public string image { get; set; }
    public string SmallDescription { get; set; }

}