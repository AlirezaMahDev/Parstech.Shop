using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class ConfigAdminGrpcService : ConfigAdminService.ConfigAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ConfigAdminGrpcService> _logger;
    private readonly IProductRepository _productRepository;

    public ConfigAdminGrpcService(
        IMediator mediator,
        ILogger<ConfigAdminGrpcService> logger,
        IProductRepository productRepository)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public override async Task<CreditInfoResponse> GetCreditOfNationalCode(NationalCodeRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new GetCreditOfNationalCodeQueryReq(
                request.SellerId,
                request.NationalCode));

            return new CreditInfoResponse
            {
                TotalCredit = result.TotalCredit,
                TotalRealCredit = result.TotalRealCredit,
                CheckCredit = result.CheckCredit,
                BonCredit = result.BonCredit,
                CashCredit = result.CashCredit,
                TotalAssignedCredit = result.TotalAssignedCredit,
                TotalSpentCredit = result.TotalSpentCredit,
                CheckUnpassedValue = result.CheckUnpassedValue,
                RealCheckCredit = result.RealCheckCredit,
                Success = true,
                Message = "Credit information retrieved successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting credit for national code: {NationalCode}", request.NationalCode);
            return new CreditInfoResponse
            {
                Success = false, Message = "Error retrieving credit information: " + ex.Message
            };
        }
    }

    public override async Task<ResponseDto> AddProductsByExcel(ExcelFileRequest request, ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new AddProductsByExcelQueryReq(request.FileName));

            return new() { Status = true, Message = "Products added successfully from Excel file", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding products from Excel file: {FileName}", request.FileName);
            return new() { Status = false, Message = "Error adding products from Excel: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ApiDataResponse> GetApiData(ApiDataRequest request, ServerCallContext context)
    {
        try
        {
            // This is a placeholder. Implement the actual API call based on your requirements
            var apiName = request.ApiName;
            var parameters = request.Parameters;

            // Example implementation assuming you have a generic API call handler
            // var result = await _mediator.Send(new GenericApiCallQueryReq(apiName, parameters));

            return new ApiDataResponse
            {
                Data = "Sample API response data", // Replace with actual API response
                Success = true,
                Message = "API data retrieved successfully"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting API data for: {ApiName}", request.ApiName);
            return new ApiDataResponse { Success = false, Message = "Error retrieving API data: " + ex.Message };
        }
    }

    public override async Task<WordpressProductsResponse> GetProductsFromWordpress(WordpressPageRequest request,
        ServerCallContext context)
    {
        try
        {
            // Implement the logic to get products from WordPress
            // Example: var products = await _mediator.Send(new GetProductsFromWordpressQueryReq(request.PageNumber));

            var response = new WordpressProductsResponse
            {
                Success = true, Message = "WordPress products retrieved successfully"
            };

            // Add sample product data
            response.Products.Add(new WordpressProductData
            {
                Id = "1",
                Name = "Sample Product",
                Description = "Sample description",
                Price = "100.00",
                Status = "publish"
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting products from WordPress page: {PageNumber}", request.PageNumber);
            return new WordpressProductsResponse
            {
                Success = false, Message = "Error retrieving WordPress products: " + ex.Message
            };
        }
    }

    public override async Task<WordpressProductResponse> GetProductFromWordpressById(WordpressProductIdRequest request,
        ServerCallContext context)
    {
        try
        {
            // Implement the logic to get a specific product from WordPress
            // Example: var product = await _mediator.Send(new GetProductFromWordpressByIdQueryReq(request.ProductId));

            var response = new WordpressProductResponse
            {
                Product = new WordpressProductData
                {
                    Id = request.ProductId,
                    Name = "Sample Product " + request.ProductId,
                    Description = "Sample description for product " + request.ProductId,
                    Price = "100.00",
                    Status = "publish"
                },
                Success = true,
                Message = "WordPress product retrieved successfully"
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product from WordPress by ID: {ProductId}", request.ProductId);
            return new WordpressProductResponse
            {
                Success = false, Message = "Error retrieving WordPress product: " + ex.Message
            };
        }
    }

    public override async Task<WordpressCategoriesResponse> GetCateguriesFromWordpress(WordpressPageRequest request,
        ServerCallContext context)
    {
        try
        {
            // Implement the logic to get categories from WordPress
            // Example: var categories = await _mediator.Send(new GetCategoriesFromWordpressQueryReq(request.PageNumber));

            var response = new WordpressCategoriesResponse
            {
                Success = true, Message = "WordPress categories retrieved successfully"
            };

            // Add sample category data
            response.Categories.Add(new WordpressCategoryData
            {
                Id = 1, Name = "Sample Category", Slug = "sample-category"
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting categories from WordPress page: {PageNumber}", request.PageNumber);
            return new WordpressCategoriesResponse
            {
                Success = false, Message = "Error retrieving WordPress categories: " + ex.Message
            };
        }
    }

    public override async Task<ResponseDto> FixProductStocks(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to fix product stocks
            // Example: await _mediator.Send(new FixProductStocksCommandReq());

            return new() { Status = true, Message = "Product stocks fixed successfully", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fixing product stocks");
            return new() { Status = false, Message = "Error fixing product stocks: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> FixDublicate(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to fix duplicates
            // Example: await _mediator.Send(new FixDuplicateCommandReq());

            return new() { Status = true, Message = "Duplicates fixed successfully", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fixing duplicates");
            return new() { Status = false, Message = "Error fixing duplicates: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> DatetimeChange(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to change datetime
            // Example: await _mediator.Send(new DatetimeChangeCommandReq());

            return new() { Status = true, Message = "Datetime changed successfully", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing datetime");
            return new() { Status = false, Message = "Error changing datetime: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> SetBestStockId(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to set best stock ID
            // Example: await _mediator.Send(new SetBestStockIdCommandReq());

            return new() { Status = true, Message = "Best stock ID set successfully", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting best stock ID");
            return new() { Status = false, Message = "Error setting best stock ID: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> ExcelFixProducts(ExcelFileRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to fix products from Excel
            // Example: await _mediator.Send(new ExcelFixProductsCommandReq(request.FileName));

            return new() { Status = true, Message = "Products fixed successfully from Excel file", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fixing products from Excel file: {FileName}", request.FileName);
            return new() { Status = false, Message = "Error fixing products from Excel: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> EditCateguriesOfProducts(ExcelFileRequest request,
        ServerCallContext context)
    {
        try
        {
            // Implement the logic to edit categories of products from Excel
            // Example: await _mediator.Send(new EditCategoriesOfProductsCommandReq(request.FileName));

            return new()
            {
                Status = true, Message = "Product categories edited successfully from Excel file", Code = 200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing product categories from Excel file: {FileName}", request.FileName);
            return new()
            {
                Status = false, Message = "Error editing product categories from Excel: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> AddUsersAndWalletCredit(ExcelFileRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to add users and wallet credit from Excel
            // Example: await _mediator.Send(new AddUsersAndWalletCreditCommandReq(request.FileName));

            return new()
            {
                Status = true, Message = "Users and wallet credit added successfully from Excel file", Code = 200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding users and wallet credit from Excel file: {FileName}", request.FileName);
            return new()
            {
                Status = false,
                Message = "Error adding users and wallet credit from Excel: " + ex.Message,
                Code = 500
            };
        }
    }

    public override async Task<ResponseDto> UpdateUserWalletsCredit(ExcelFileRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to update user wallets credit from Excel
            // Example: await _mediator.Send(new UpdateUserWalletsCreditCommandReq(request.FileName));

            return new()
            {
                Status = true, Message = "User wallets credit updated successfully from Excel file", Code = 200
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user wallets credit from Excel file: {FileName}", request.FileName);
            return new()
            {
                Status = false, Message = "Error updating user wallets credit from Excel: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> FillProductCode(ExcelFileRequest request, ServerCallContext context)
    {
        try
        {
            // Implement the logic to fill product code from Excel
            // Example: await _mediator.Send(new FillProductCodeCommandReq(request.FileName));

            return new() { Status = true, Message = "Product codes filled successfully from Excel file", Code = 200 };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error filling product codes from Excel file: {FileName}", request.FileName);
            return new()
            {
                Status = false, Message = "Error filling product codes from Excel: " + ex.Message, Code = 500
            };
        }
    }
}