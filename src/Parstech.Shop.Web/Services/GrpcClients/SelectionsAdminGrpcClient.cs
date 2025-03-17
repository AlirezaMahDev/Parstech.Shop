using Grpc.Core;
using Grpc.Net.Client;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class SelectionsAdminGrpcClient : ISelectionsAdminGrpcClient
{
    private readonly ILogger<SelectionsAdminGrpcClient> _logger;
    private readonly SelectionsAdminService.SelectionsAdminServiceClient _client;

    public SelectionsAdminGrpcClient(IConfiguration configuration, ILogger<SelectionsAdminGrpcClient> logger)
    {
        _logger = logger;
        GrpcChannel? channel = GrpcChannel.ForAddress(configuration["GrpcSettings:ApiAddress"]);
        _client = new SelectionsAdminService.SelectionsAdminServiceClient(channel);
    }

    public async Task<List<SectionDto>> GetDiscountSectionsSelectAsync()
    {
        try
        {
            var response = await _client.GetDiscountSectionsSelectAsync(new EmptyRequest());

            var result = new List<SectionDto>();
            foreach (var section in response.Sections)
            {
                result.Add(new SectionDto
                {
                    Id = section.Id,
                    Title = section.Title,
                    SectionName = section.SectionName,
                    Order = section.Order,
                    IsActive = section.IsActive,
                    IsDiscount = section.IsDiscount
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service GetDiscountSectionsSelect");
            throw;
        }
    }

    public async Task<List<ProductSelectDto>> GetProductsSelectAsync()
    {
        try
        {
            var response = await _client.GetProductsSelectAsync(new EmptyRequest());

            var result = new List<ProductSelectDto>();
            foreach (var product in response.Products)
            {
                result.Add(new ProductSelectDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    LatinName = product.LatinName,
                    TypeId = product.TypeId,
                    TypeName = product.TypeName,
                    StoreId = product.StoreId,
                    StoreName = product.StoreName,
                    Price = product.Price,
                    SalePrice = product.SalePrice
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service GetProductsSelect");
            throw;
        }
    }

    public async Task<List<CategurySelectDto>> GetCategoriesSelectAsync()
    {
        try
        {
            var response = await _client.GetCategoriesSelectAsync(new EmptyRequest());

            var result = new List<CategurySelectDto>();
            foreach (var category in response.Categories)
            {
                result.Add(new CategurySelectDto
                {
                    Id = category.Id,
                    Title = category.Title,
                    LatinTitle = category.LatinTitle,
                    ParentId = category.ParentId,
                    IsParnet = category.IsParent,
                    Image = category.Image
                });
            }

            return result;
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Error calling gRPC service GetCategoriesSelect");
            throw;
        }
    }
}