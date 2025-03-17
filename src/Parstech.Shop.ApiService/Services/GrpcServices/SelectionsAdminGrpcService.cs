using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.ApiService.Services.GrpcServices;

public class SelectionsAdminGrpcService : SelectionsAdminService.SelectionsAdminServiceBase
{
    private readonly IMediator _mediator;

    public SelectionsAdminGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<SectionListResponse> GetDiscountSectionsSelect(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            var sections = await _mediator.Send(new DiscountSectionsSelectQueryReq());

            var response = new SectionListResponse { IsSuccess = true };

            foreach (var section in sections)
            {
                response.Sections.Add(new SectionDto
                {
                    Id = section.Id,
                    Title = section.Title ?? string.Empty,
                    SectionName = section.SectionName ?? string.Empty,
                    Order = section.Order,
                    IsActive = section.IsActive,
                    IsDiscount = section.IsDiscount
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ProductSelectListResponse> GetProductsSelect(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            void products = await _mediator.Send(new ProductSelectListQueryReq());

            var response = new ProductSelectListResponse { IsSuccess = true };

            foreach (var product in products)
            {
                response.Products.Add(new ProductSelectDto
                {
                    Id = product.Id,
                    Name = product.Name ?? string.Empty,
                    Image = product.Image ?? string.Empty,
                    LatinName = product.LatinName ?? string.Empty,
                    TypeId = product.TypeId,
                    TypeName = product.TypeName ?? string.Empty,
                    StoreId = product.StoreId,
                    StoreName = product.StoreName ?? string.Empty,
                    Price = product.Price,
                    SalePrice = product.SalePrice
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<CategorySelectListResponse> GetCategoriesSelect(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            void categories = await _mediator.Send(new CategurySelectListQueryReq());

            var response = new CategorySelectListResponse { IsSuccess = true };

            foreach (var category in categories)
            {
                response.Categories.Add(new CategorySelectDto
                {
                    Id = category.Id,
                    Title = category.Title ?? string.Empty,
                    LatinTitle = category.LatinTitle ?? string.Empty,
                    ParentId = category.ParentId,
                    IsParent = category.IsParnet,
                    Image = category.Image ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }
}