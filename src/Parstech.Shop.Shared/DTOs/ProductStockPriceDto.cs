namespace Parstech.Shop.ApiService.Application.DTOs;

public class ProductStockPriceDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public int TypeId { get; set; }
    public string ProductName { get; set; }

    public long Price { get; set; }
    public string TextPrice { get; set; }

    public long SalePrice { get; set; }
    public string TextSalePrice { get; set; }

    public long DiscountPrice { get; set; }
    public string TextDiscountPrice { get; set; }

    public long BasePrice { get; set; }
    public string TextBasePrice { get; set; }

    public bool StockStatus { get; set; }

    public int Quantity { get; set; }
    public int QuantityPerBundle { get; set; }

    public int MaximumSaleInOrder { get; set; }

    public int StoreId { get; set; }
    public string StoreName { get; set; }

    public int RepId { get; set; }
    public string RepName { get; set; }

    public int TaxId { get; set; }
    public DateTime? DiscountDate { get; set; }
    public string DiscountDateShamsi { get; set; }
    public int? CateguryOfUserId { get; set; }

    public string? CateguryOfUserType { get; set; }

    public bool? ShowInDiscountPanels { get; set; }
    public long BestPriceForBestStock { get; set; }
}

public class ProductStockPriceStoreDto
{
    public int Id { get; set; }
    public string StoreName { get; set; }
}

public class ProductDiscountParameterDto
{
    public int CurrentPage { get; set; }
    public int TakePage { get; set; }
    public int PageCount { get; set; }
    public string Filter { get; set; }
    public int RepId { get; set; }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SectionId { get; set; }
    public string ShowInDiscountPanel { get; set; }
    public string Type { get; set; }
    public string Exist { get; set; }
    public int Categury { get; set; }
}

public class ProductDiscountPagingDto
{
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }

    public Array List { get; set; }
}