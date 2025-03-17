using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Command;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class UserProductService : UserProductServiceBase
{
    private readonly IMediator _mediator;
    private readonly IUserProductRepository _userProductRepository;

    public UserProductService(IMediator mediator, IUserProductRepository userProductRepository)
    {
        _mediator = mediator;
        _userProductRepository = userProductRepository;
    }

    public override async Task<UserProductResponse> CreateUserProduct(CreateUserProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var command = new CreateUserProductCommandReq(request.UserName, request.ProductId, request.Type);
            void result = await _mediator.Send(command);

            if (result)
            {
                var userProduct =
                    await _userProductRepository.GetUserProductByUserAndProduct(request.UserName, request.ProductId);
                return new UserProductResponse
                {
                    Success = true,
                    Message = $"محصول به {request.Type} افزوده شد",
                    Data = new UserProduct
                    {
                        Id = userProduct.Id,
                        UserName = userProduct.UserName,
                        ProductId = userProduct.ProductId,
                        Type = userProduct.Type,
                        ProductName = userProduct.ProductName,
                        Price = userProduct.Price,
                        Image = userProduct.Image,
                        CreatedAt = userProduct.CreatedAt.ToString()
                    }
                };
            }

            return new UserProductResponse { Success = false, Message = "خطا در افزودن محصول" };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<UserProductResponse> DeleteUserProduct(DeleteUserProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var command = new DeleteUserProductCommandReq(request.UserProductId);
            await _mediator.Send(command);

            return new UserProductResponse { Success = true, Message = "محصول با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<GetUserProductsResponse> GetUserProducts(GetUserProductsRequest request,
        ServerCallContext context)
    {
        try
        {
            var query = new GetUserProductsQueryReq(request.UserName, request.Type);
            void products = await _mediator.Send(query);

            var response = new GetUserProductsResponse();
            response.Products.AddRange(products.Select(p => new UserProduct
            {
                Id = p.Id,
                UserName = p.UserName,
                ProductId = p.ProductId,
                Type = p.Type,
                ProductName = p.ProductName,
                Price = p.Price,
                Image = p.Image,
                CreatedAt = p.CreatedAt.ToString()
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}