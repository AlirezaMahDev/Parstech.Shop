using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class ConfigAdminGrpcClient : GrpcClientBase, IConfigAdminGrpcClient
{
    private readonly ConfigAdminService.ConfigAdminServiceClient _client;

    public ConfigAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new ConfigAdminService.ConfigAdminServiceClient(Channel);
    }

    public async Task<CreditInfoDto> GetCreditOfNationalCodeAsync(int sellerId, string nationalCode)
    {
        var request = new NationalCodeRequest { SellerId = sellerId, NationalCode = nationalCode };

        var response = await _client.GetCreditOfNationalCodeAsync(request);

        return new()
        {
            TotalCredit = response.TotalCredit,
            TotalRealCredit = response.TotalRealCredit,
            CheckCredit = response.CheckCredit,
            BonCredit = response.BonCredit,
            CashCredit = response.CashCredit,
            TotalAssignedCredit = response.TotalAssignedCredit,
            TotalSpentCredit = response.TotalSpentCredit,
            CheckUnpassedValue = response.CheckUnpassedValue,
            RealCheckCredit = response.RealCheckCredit
        };
    }

    public async Task<ResponseDto> AddProductsByExcelAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.AddProductsByExcelAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<string> GetApiDataAsync(string apiName, Dictionary<string, string> parameters)
    {
        var request = new ApiDataRequest { ApiName = apiName };

        foreach (KeyValuePair<string, string> param in parameters)
        {
            request.Parameters.Add(param.Key, param.Value);
        }

        var response = await _client.GetApiDataAsync(request);

        if (!response.Success)
        {
            throw new($"API call failed: {response.Message}");
        }

        return response.Data;
    }

    public async Task<List<WordpressProductDto>> GetProductsFromWordpressAsync(int pageNumber)
    {
        var request = new WordpressPageRequest { PageNumber = pageNumber };
        var response = await _client.GetProductsFromWordpressAsync(request);

        if (!response.Success)
        {
            throw new($"Failed to get WordPress products: {response.Message}");
        }

        List<WordpressProductDto> products = new();

        foreach (var product in response.Products)
        {
            products.Add(MapToWordpressProductDto(product));
        }

        return products;
    }

    public async Task<WordpressProductDto> GetProductFromWordpressByIdAsync(string productId)
    {
        var request = new WordpressProductIdRequest { ProductId = productId };
        var response = await _client.GetProductFromWordpressByIdAsync(request);

        if (!response.Success)
        {
            throw new($"Failed to get WordPress product: {response.Message}");
        }

        return MapToWordpressProductDto(response.Product);
    }

    public async Task<List<WordpressCategoryDto>> GetCateguriesFromWordpressAsync(int pageNumber)
    {
        var request = new WordpressPageRequest { PageNumber = pageNumber };
        var response = await _client.GetCateguriesFromWordpressAsync(request);

        if (!response.Success)
        {
            throw new($"Failed to get WordPress categories: {response.Message}");
        }

        List<WordpressCategoryDto> categories = new();

        foreach (var category in response.Categories)
        {
            categories.Add(new() { Id = category.Id, Name = category.Name, Slug = category.Slug });
        }

        return categories;
    }

    public async Task<ResponseDto> FixProductStocksAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.FixProductStocksAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> FixDublicateAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.FixDublicateAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DatetimeChangeAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.DatetimeChangeAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> SetBestStockIdAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.SetBestStockIdAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> ExcelFixProductsAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.ExcelFixProductsAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> EditCateguriesOfProductsAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.EditCateguriesOfProductsAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> AddUsersAndWalletCreditAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.AddUsersAndWalletCreditAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdateUserWalletsCreditAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.UpdateUserWalletsCreditAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> FillProductCodeAsync(string fileName)
    {
        var request = new ExcelFileRequest { FileName = fileName };
        var response = await _client.FillProductCodeAsync(request);

        return new() { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    private WordpressProductDto MapToWordpressProductDto(WordpressProductData product)
    {
        WordpressProductDto productDto = new()
        {
            Id = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            Permalink = product.Permalink,
            Sku = product.Sku,
            Price = product.Price,
            RegularPrice = product.RegularPrice,
            SalePrice = product.SalePrice,
            Status = product.Status,
            Featured = product.Featured,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Images = new(),
            Categories = new(),
            Attributes = new()
        };

        foreach (var image in product.Images)
        {
            productDto.Images.Add(image);
        }

        foreach (var category in product.Categories)
        {
            productDto.Categories.Add(new() { Id = category.Id, Name = category.Name, Slug = category.Slug });
        }

        foreach (var attribute in product.Attributes)
        {
            WordpressAttributeDto attributeDto = new() { Id = attribute.Id, Name = attribute.Name, Options = new() };

            foreach (var option in attribute.Options)
            {
                attributeDto.Options.Add(option);
            }

            productDto.Attributes.Add(attributeDto);
        }

        return productDto;
    }
}

public class CreditInfoDto
{
    public int TotalCredit { get; set; }
    public int TotalRealCredit { get; set; }
    public int CheckCredit { get; set; }
    public int BonCredit { get; set; }
    public int CashCredit { get; set; }
    public int TotalAssignedCredit { get; set; }
    public int TotalSpentCredit { get; set; }
    public int CheckUnpassedValue { get; set; }
    public int RealCheckCredit { get; set; }
    public List<string> AvailableBons { get; set; } = new();
}

public class WordpressProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Permalink { get; set; }
    public string Sku { get; set; }
    public string Price { get; set; }
    public string RegularPrice { get; set; }
    public string SalePrice { get; set; }
    public string Status { get; set; }
    public bool Featured { get; set; }
    public string Description { get; set; }
    public string ShortDescription { get; set; }
    public List<string> Images { get; set; } = new();
    public List<WordpressCategoryDto> Categories { get; set; } = new();
    public List<WordpressAttributeDto> Attributes { get; set; } = new();
}

public class WordpressCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
}

public class WordpressAttributeDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> Options { get; set; } = new();
}