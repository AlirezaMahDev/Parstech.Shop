namespace Parstech.Shop.ApiService.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public int ProductStockPriceId { get; set; }
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;
    public string? LatinName { get; set; }

    public string? Code { get; set; }

    public long Price { get; set; }

    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }
    public DateTime? DiscountDate { get; set; }

    public long BasePrice { get; set; }

    public bool StockStatus { get; set; }

    public int Quantity { get; set; }
    public int MaximumSaleInOrder { get; set; }

    public int Score { get; set; }

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public string? ShortLink { get; set; }

    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public string? VariationName { get; set; }

    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string LatinStoreName { get; set; }

    public string Image { get; set; }

    public int? ParentId { get; set; }
    public string ParentProductName { get; set; }

    public int BrandId { get; set; }
    public string BrandName { get; set; }
    public string LatinBrandName { get; set; }

    public int TaxId { get; set; }
    public int RepId { get; set; }
    public string RepName { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateDateShamsi { get; set; }

    public int Visit { get; set; }
    public int CateguryId { get; set; }
    public string CateguryName { get; set; }
    public string CateguryLatinName { get; set; }
    public int CountSale { get; set; }
    public bool SingleSale { get; set; }

    public int? QuantityPerBundle { get; set; }

    public string? TaxCode { get; set; }
    public string? Keywords { get; set; }

    public bool IsActive { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
    public int? CateguryOfUserId { get; set; }
    public string CateguryOfUserName { get; set; }
    public string CateguryOfUserType { get; set; }
    public int? BestStockId { get; set; }
    public int? BestStockUserCateguryId { get; set; }
}

public class ProductPageingDto
{
    public int CurrentPage { get; set; }

    public int PageCount { get; set; }
    public int Searched { get; set; }
    public Array ProductDtos { get; set; }
    public List<ProductDto> ProductList { get; set; }
}

public class ProductParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public string Store { get; set; }
    public string Categury { get; set; }
    public string Brand { get; set; }
    public string Type { get; set; }
}

public class ProductListShowDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ProductStockPriceId { get; set; }
    public string Name { get; set; }
    public string? LatinName { get; set; }
    public string Image { get; set; }
    public string CateguryName { get; set; }
    public string CateguryLatinName { get; set; }
    public int Quantity { get; set; }
    public long SalePrice { get; set; }
    public long DiscountPrice { get; set; }
    public DateTime? DiscountDate { get; set; }
    public string? ShortDescription { get; set; }
    public string? ShortLink { get; set; }
    public string? VariationName { get; set; }
    public int SectionId { get; set; }
    public string SectionName { get; set; }
    public bool? ShowInDiscountPanels { get; set; }
}

public class ProductDetailShowDto
{
    public int Id { get; set; }
    public int ProductStockPriceId { get; set; }
    public string? ShortLink { get; set; }
    public string Name { get; set; }
    public string? LatinName { get; set; }
    public long SalePrice { get; set; }
    public long DiscountPrice { get; set; }
    public int Score { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public string? VariationName { get; set; }
    public List<ProductDto> RelatedStores { get; set; }
    public List<BaseProductPropertyDto> Properties { get; set; }
    public List<ProductPropertyDto> SomeProperties { get; set; }
    public List<ProductDto> Accessories { get; set; }
    public List<ProductDto> Childs { get; set; }
    public BrandDto Brand { get; set; }
    public string Store { get; set; }
    public string StoreLatin { get; set; }
    public DateTime? DiscountDate { get; set; }
    public int? CateguryOfUserId { get; set; }
}

public class ProductSelectDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Code { get; set; }
}

public class ProductQuickEditDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? LatinName { get; set; }
    public int TypeId { get; set; }
    public string? VariationName { get; set; }
    public int StoreId { get; set; }
    public int? ParentId { get; set; }
    public int BrandId { get; set; }
    public int TaxId { get; set; }
    public int Score { get; set; }
    public int? QuantityPerBundle { get; set; }
}

public class ProductExcelDto
{
    public string Id { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; }
    public string Parent { get; set; }
    public string Description { get; set; }
    public string brandId { get; set; }
}

public class ChildsAndStock
{
    public List<ProductDto> ProductDtos { get; set; }
    public List<ProductStockPriceDto> ProductStockDtos { get; set; }
}

public class ProductSearchParameterDto
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string Filter { get; set; }
    public string Store { get; set; }
    public string Categury { get; set; }
    public int CateguryId { get; set; }
    public long MinPrice { get; set; }
    public long MaxPrice { get; set; }
    public bool Exist { get; set; }

    public string Type { get; set; }
    public string IsActive { get; set; }
    public string Brand { get; set; }
}

public class ProductPaginationCateguryDto
{
    public int ProductStockPriceId { get; set; }
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public long SalePrice { get; set; }

    public long DiscountPrice { get; set; }

    public int Quantity { get; set; }
    public int TypeId { get; set; }
    public string Image { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string ShortLink { get; set; }
    public long FinalPrice { get; set; }
    public long FinalDiscountPrice { get; set; }
    public long FinalQuantity { get; set; }
}