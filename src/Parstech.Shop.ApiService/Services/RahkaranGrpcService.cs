using AutoMapper;

using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class RahkaranGrpcService : RahkaranService.RahkaranServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public RahkaranGrpcService(IMediator mediator, IMapper mapper, IConfiguration configuration)
    {
        _mediator = mediator;
        _mapper = mapper;
        _configuration = configuration;
    }

    public override async Task<RahkaranAllResponse> GetAllRahkaranData(GetRahkaranDataRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahakaranAllQueryReq(request.OrderId));

            var response = new RahkaranAllResponse();

            if (result.order != null)
            {
                response.Order = new Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                {
                    OrderId = result.order.OrderId,
                    OrderCode = result.order.OrderCode,
                    RahkaranPishNumber = result.order.RahkaranPishNumber ?? string.Empty,
                    RahakaranFactorNumber = result.order.RahakaranFactorNumber ?? string.Empty,
                    RahakaranFactorSerial = result.order.RahakaranFactorSerial ?? string.Empty
                };
            }

            if (result.customer != null)
            {
                response.Customer = new Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                {
                    Id = result.customer.Id,
                    UserName = result.customer.UserName,
                    FirstName = result.customer.FirstName ?? string.Empty,
                    LastName = result.customer.LastName ?? string.Empty,
                    EconomicCode = result.customer.EconomicCode ?? string.Empty,
                    NationalCode = result.customer.NationalCode ?? string.Empty,
                    UserId = result.customer.UserId ?? 0,
                    RahkaranUserId = result.customer.RahkaranUserId ?? string.Empty
                };
            }

            if (result.products != null)
            {
                foreach (var product in result.products)
                {
                    response.Products.Add(new Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                    {
                        StockId = product.StockId ?? 0,
                        DetailId = product.DetailId ?? 0,
                        Count = product.Count ?? 0,
                        Price = product.Price ?? 0,
                        Name = product.Name,
                        Code = product.Code ?? string.Empty,
                        VariationName = product.VariationName ?? string.Empty,
                        ProductId = product.ProductId ?? 0,
                        RahkaranProductId = product.RahkaranProductId ?? string.Empty,
                        RahkaranUnitId = product.RahkaranUnitId ?? 0
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranOrderResponse> GetRahkaranOrder(GetRahkaranOrderRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahkaranOrderReadCommandReq(request.Id));

            var response = new RahkaranOrderResponse { IsSuccess = true, Message = "Order retrieved successfully" };

            if (result != null)
            {
                response.Order = new Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                {
                    OrderId = result.OrderId,
                    OrderCode = result.OrderCode,
                    RahkaranPishNumber = result.RahkaranPishNumber ?? string.Empty,
                    RahakaranFactorNumber = result.RahakaranFactorNumber ?? string.Empty,
                    RahakaranFactorSerial = result.RahakaranFactorSerial ?? string.Empty
                };
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranOrderResponse> CreateRahkaranOrder(CreateRahkaranOrderRequest request,
        ServerCallContext context)
    {
        try
        {
            var orderDto = new RahkaranOrderDto
            {
                OrderId = request.Order.OrderId,
                OrderCode = request.Order.OrderCode,
                RahkaranPishNumber = request.Order.RahkaranPishNumber,
                RahakaranFactorNumber = request.Order.RahakaranFactorNumber,
                RahakaranFactorSerial = request.Order.RahakaranFactorSerial
            };

            void result = await _mediator.Send(new RahkaranOrderCreateCommandReq(orderDto));

            var response = new RahkaranOrderResponse
            {
                IsSuccess = true,
                Message = "Order created successfully",
                Order = new Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                {
                    OrderId = result.OrderId,
                    OrderCode = result.OrderCode,
                    RahkaranPishNumber = result.RahkaranPishNumber ?? string.Empty,
                    RahakaranFactorNumber = result.RahakaranFactorNumber ?? string.Empty,
                    RahakaranFactorSerial = result.RahakaranFactorSerial ?? string.Empty
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranOrderResponse> UpdateRahkaranOrder(UpdateRahkaranOrderRequest request,
        ServerCallContext context)
    {
        try
        {
            var orderDto = new RahkaranOrderDto
            {
                OrderId = request.Order.OrderId,
                OrderCode = request.Order.OrderCode,
                RahkaranPishNumber = request.Order.RahkaranPishNumber,
                RahakaranFactorNumber = request.Order.RahakaranFactorNumber,
                RahakaranFactorSerial = request.Order.RahakaranFactorSerial
            };

            void result = await _mediator.Send(new RahkaranOrderUpdateCommandReq(orderDto));

            var response = new RahkaranOrderResponse
            {
                IsSuccess = true,
                Message = "Order updated successfully",
                Order = new Shop.Shared.Protos.Rahkaran.RahkaranOrderDto
                {
                    OrderId = result.OrderId,
                    OrderCode = result.OrderCode,
                    RahkaranPishNumber = result.RahkaranPishNumber ?? string.Empty,
                    RahakaranFactorNumber = result.RahakaranFactorNumber ?? string.Empty,
                    RahakaranFactorSerial = result.RahakaranFactorSerial ?? string.Empty
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    // User operations
    public override async Task<RahkaranUserResponse> GetRahkaranUser(GetRahkaranUserRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahkaranUserReadCommandReq(request.Id));

            var response = new RahkaranUserResponse { IsSuccess = true, Message = "User retrieved successfully" };

            if (result != null)
            {
                response.User = new Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FirstName = result.FirstName ?? string.Empty,
                    LastName = result.LastName ?? string.Empty,
                    EconomicCode = result.EconomicCode ?? string.Empty,
                    NationalCode = result.NationalCode ?? string.Empty,
                    UserId = result.UserId ?? 0,
                    RahkaranUserId = result.RahkaranUserId ?? string.Empty
                };
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranUserResponse> CreateRahkaranUser(CreateRahkaranUserRequest request,
        ServerCallContext context)
    {
        try
        {
            var userDto = new RahkaranUserDto
            {
                Id = request.User.Id,
                UserName = request.User.UserName,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                EconomicCode = request.User.EconomicCode,
                NationalCode = request.User.NationalCode,
                UserId = request.User.UserId,
                RahkaranUserId = request.User.RahkaranUserId
            };

            void result = await _mediator.Send(new RahkaranUserCreateCommandReq(userDto));

            var response = new RahkaranUserResponse
            {
                IsSuccess = true,
                Message = "User created successfully",
                User = new Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FirstName = result.FirstName ?? string.Empty,
                    LastName = result.LastName ?? string.Empty,
                    EconomicCode = result.EconomicCode ?? string.Empty,
                    NationalCode = result.NationalCode ?? string.Empty,
                    UserId = result.UserId ?? 0,
                    RahkaranUserId = result.RahkaranUserId ?? string.Empty
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranUserResponse> UpdateRahkaranUser(UpdateRahkaranUserRequest request,
        ServerCallContext context)
    {
        try
        {
            var userDto = new RahkaranUserDto
            {
                Id = request.User.Id,
                UserName = request.User.UserName,
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                EconomicCode = request.User.EconomicCode,
                NationalCode = request.User.NationalCode,
                UserId = request.User.UserId,
                RahkaranUserId = request.User.RahkaranUserId
            };

            void result = await _mediator.Send(new RahkaranUserUpdateCommandReq(userDto));

            var response = new RahkaranUserResponse
            {
                IsSuccess = true,
                Message = "User updated successfully",
                User = new Shop.Shared.Protos.Rahkaran.RahkaranUserDto
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FirstName = result.FirstName ?? string.Empty,
                    LastName = result.LastName ?? string.Empty,
                    EconomicCode = result.EconomicCode ?? string.Empty,
                    NationalCode = result.NationalCode ?? string.Empty,
                    UserId = result.UserId ?? 0,
                    RahkaranUserId = result.RahkaranUserId ?? string.Empty
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    // Product operations
    public override async Task<RahkaranProductResponse> GetRahkaranProduct(GetRahkaranProductRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahkaranProductReadCommandReq(request.Id));

            var response = new RahkaranProductResponse { IsSuccess = true, Message = "Product retrieved successfully" };

            if (result != null)
            {
                response.Product = new Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                {
                    StockId = result.StockId ?? 0,
                    DetailId = result.DetailId ?? 0,
                    Count = result.Count ?? 0,
                    Price = result.Price ?? 0,
                    Name = result.Name,
                    Code = result.Code ?? string.Empty,
                    VariationName = result.VariationName ?? string.Empty,
                    ProductId = result.ProductId ?? 0,
                    RahkaranProductId = result.RahkaranProductId ?? string.Empty,
                    RahkaranUnitId = result.RahkaranUnitId ?? 0
                };
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranProductResponse> CreateRahkaranProduct(CreateRahkaranProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var productDto = new RahkaranProductDto
            {
                StockId = request.Product.StockId,
                DetailId = request.Product.DetailId,
                Count = request.Product.Count,
                Price = request.Product.Price,
                Name = request.Product.Name,
                Code = request.Product.Code,
                VariationName = request.Product.VariationName,
                ProductId = request.Product.ProductId,
                RahkaranProductId = request.Product.RahkaranProductId,
                RahkaranUnitId = request.Product.RahkaranUnitId
            };

            void result = await _mediator.Send(new RahkaranProductCreateCommandReq(productDto));

            var response = new RahkaranProductResponse
            {
                IsSuccess = true,
                Message = "Product created successfully",
                Product = new Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                {
                    StockId = result.StockId ?? 0,
                    DetailId = result.DetailId ?? 0,
                    Count = result.Count ?? 0,
                    Price = result.Price ?? 0,
                    Name = result.Name,
                    Code = result.Code ?? string.Empty,
                    VariationName = result.VariationName ?? string.Empty,
                    ProductId = result.ProductId ?? 0,
                    RahkaranProductId = result.RahkaranProductId ?? string.Empty,
                    RahkaranUnitId = result.RahkaranUnitId ?? 0
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RahkaranProductResponse> UpdateRahkaranProduct(UpdateRahkaranProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var productDto = new RahkaranProductDto
            {
                StockId = request.Product.StockId,
                DetailId = request.Product.DetailId,
                Count = request.Product.Count,
                Price = request.Product.Price,
                Name = request.Product.Name,
                Code = request.Product.Code,
                VariationName = request.Product.VariationName,
                ProductId = request.Product.ProductId,
                RahkaranProductId = request.Product.RahkaranProductId,
                RahkaranUnitId = request.Product.RahkaranUnitId
            };

            void result = await _mediator.Send(new RahkaranProductUpdateCommandReq(productDto));

            var response = new RahkaranProductResponse
            {
                IsSuccess = true,
                Message = "Product updated successfully",
                Product = new Shop.Shared.Protos.Rahkaran.RahkaranProductDto
                {
                    StockId = result.StockId ?? 0,
                    DetailId = result.DetailId ?? 0,
                    Count = result.Count ?? 0,
                    Price = result.Price ?? 0,
                    Name = result.Name,
                    Code = result.Code ?? string.Empty,
                    VariationName = result.VariationName ?? string.Empty,
                    ProductId = result.ProductId ?? 0,
                    RahkaranProductId = result.RahkaranProductId ?? string.Empty,
                    RahkaranUnitId = result.RahkaranUnitId ?? 0
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    // API operations
    public override async Task<ApiResponse> SendOrderToApi(SendOrderToApiRequest request, ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahkaranSendOrderToApiQueryReq(request.OrderId));

            var response = new ApiResponse { IsSuccess = result.IsSuccessed, Message = result.Message };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ApiResponse> FollowOrderFromApi(FollowOrderFromApiRequest request,
        ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new RahkaranFollowOrderFromApiQueryReq(request.OrderId));

            var response = new ApiResponse { IsSuccess = result.IsSuccessed, Message = result.Message };

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }
}