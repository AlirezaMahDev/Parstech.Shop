using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Query;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class UserProductGrpcService : UserProductService.UserProductServiceBase
{
    private readonly IMediator _mediator;

    public UserProductGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<UserProductResponse> CreateUserProduct(CreateUserProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var userProductDto = new Shop.Application.DTOs.UserProduct.UserProductDto
            {
                UserName = request.UserName, ProductId = request.ProductId, Type = request.Type
            };

            void result = await _mediator.Send(new UserProductCreateCommandReq(userProductDto));

            return new UserProductResponse
            {
                Success = true,
                Message = "محصول با موفقیت به لیست شما اضافه شد",
                Data = new UserProduct
                {
                    Id = result.Id,
                    UserName = result.UserName ?? string.Empty,
                    ProductId = result.ProductId,
                    Type = result.Type ?? string.Empty,
                    ProductName = result.ProductName ?? string.Empty,
                    Price = result.Price,
                    Image = result.Image ?? string.Empty,
                    CreatedAt = result.CreatedAt?.ToString() ?? string.Empty
                }
            };
        }
        catch (Exception ex)
        {
            return new UserProductResponse { Success = false, Message = $"خطا در افزودن محصول به لیست: {ex.Message}" };
        }
    }

    public override async Task<UserProductResponse> DeleteUserProduct(DeleteUserProductRequest request,
        ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new UserProductDeleteCommandReq(request.UserProductId));

            return new UserProductResponse { Success = true, Message = "محصول با موفقیت از لیست شما حذف شد" };
        }
        catch (Exception ex)
        {
            return new UserProductResponse { Success = false, Message = $"خطا در حذف محصول از لیست: {ex.Message}" };
        }
    }

    public override async Task<GetUserProductsResponse> GetUserProducts(GetUserProductsRequest request,
        ServerCallContext context)
    {
        try
        {
            void products = request.Type.ToLower() switch
            {
                "favorite" => await _mediator.Send(new GetFavoriteProductOfUsersQueryReq(request.UserName)),
                "compare" => await _mediator.Send(new GetCmparisonProductsOfUsersQueryReq(request.UserName)),
                _ => throw new RpcException(new(StatusCode.InvalidArgument,
                    "Invalid product type. Use 'Favorite' or 'Compare'."))
            };

            var response = new GetUserProductsResponse();
            foreach (var product in products)
            {
                response.Products.Add(new UserProduct
                {
                    Id = product.Id,
                    UserName = request.UserName,
                    ProductId = product.Id,
                    Type = request.Type,
                    ProductName = product.Name ?? string.Empty,
                    Price = product.Price,
                    Image = product.Image ?? string.Empty,
                    CreatedAt = DateTime.Now.ToString() // We don't have this info from the query
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