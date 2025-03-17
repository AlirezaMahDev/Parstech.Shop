using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class TorobGrpcService : TorobService.TorobServiceBase
{
    private readonly IMediator _mediator;

    public TorobGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<Torob> GetTorobProduct(TorobRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new TorobGetProductByIdQueryReq(request.StoreId));

            return new Torob
            {
                ProductId = result.product_id ?? string.Empty,
                PageUrl = result.page_url ?? string.Empty,
                Price = result.price ?? string.Empty,
                Availability = result.availability ?? string.Empty,
                OldPrice = result.old_price ?? string.Empty,
                Image = result.image ?? string.Empty,
                Content = result.content ?? string.Empty,
                Name = result.name ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<TorobProductsResponse> GetTorobProducts(TorobProductsRequest request,
        ServerCallContext context)
    {
        try
        {
            var products = await _mediator.Send(new TorobGetProductsQueryReq(request.Page));

            var response = new TorobProductsResponse();
            foreach (var product in products)
            {
                response.Products.Add(new TorobProduct
                {
                    Name = product.Name ?? string.Empty,
                    Id = product.Id,
                    ProductId = product.ProductId,
                    SalePrice = product.SalePrice,
                    DiscountPrice = product.DiscountPrice,
                    Quantity = product.Quantity,
                    TypeId = product.TypeId,
                    ParentId = product.ParentId,
                    ShortLink = product.ShortLink ?? string.Empty,
                    DiscountDate = product.DiscountDate,
                    ShortDescription = product.ShortDescription ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}