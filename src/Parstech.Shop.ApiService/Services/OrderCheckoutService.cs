using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.OrderCheckout;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Commands;
using Shop.Application.Features.OrderDetail.Requests.Queries;

namespace Shop.ApiService.Services
{
    public class OrderCheckoutService : OrderCheckoutServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderCheckoutService(
            IMediator mediator,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public override async Task<OrderResponse> GetOpenOrder(OpenOrderRequest request, ServerCallContext context)
        {
            try
            {
                var order = await _mediator.Send(new GetOpenOrderOfUserQueryReq(request.UserName));
                
                return new OrderResponse
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    UserName = order.UserName,
                    CreateDate = order.CreateDate.ToString(),
                    IsPaid = order.IsPaid,
                    Total = order.Total,
                    Discount = order.Discount,
                    FinalPrice = order.FinalPrice,
                    TrackingCode = order.TrackingCode
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<RefreshOrderResponse> RefreshOrder(RefreshOrderRequest request, ServerCallContext context)
        {
            try
            {
                await _mediator.Send(new RefreshOrderQueryReq(request.OrderId));
                var order = await _orderRepository.GetOrderById(request.OrderId);
                
                return new RefreshOrderResponse
                {
                    Status = true,
                    Message = "Order refreshed successfully",
                    Order = new OrderResponse
                    {
                        OrderId = order.OrderId,
                        UserId = order.UserId,
                        UserName = order.UserName,
                        CreateDate = order.CreateDate.ToString(),
                        IsPaid = order.IsPaid,
                        Total = order.Total,
                        Discount = order.Discount,
                        FinalPrice = order.FinalPrice,
                        TrackingCode = order.TrackingCode
                    }
                };
            }
            catch (Exception ex)
            {
                return new RefreshOrderResponse
                {
                    Status = false,
                    Message = $"Error refreshing order: {ex.Message}"
                };
            }
        }

        public override async Task<OrderDetailsResponse> GetOrderDetails(OrderDetailsRequest request, ServerCallContext context)
        {
            try
            {
                var orderDetails = await _mediator.Send(new OrderDetailShowQueryReq(request.OrderId));
                
                var response = new OrderDetailsResponse
                {
                    OrderId = orderDetails.OrderId,
                    UserName = orderDetails.UserName,
                    Total = orderDetails.Total,
                    Discount = orderDetails.Discount,
                    FinalPrice = orderDetails.FinalPrice,
                    ShippingId = orderDetails.ShippingId,
                    ShippingAddress = orderDetails.ShippingAddress,
                    ShippingPostalCode = orderDetails.ShippingPostalCode,
                    ShippingMobile = orderDetails.ShippingMobile
                };
                
                // Add order details
                foreach (var detail in orderDetails.OrderDetails)
                {
                    response.Details.Add(new OrderDetailItem
                    {
                        Id = detail.Id,
                        OrderId = detail.OrderId,
                        ProductId = detail.ProductId,
                        ProductName = detail.ProductName,
                        ProductImage = detail.ProductImage,
                        Count = detail.Count,
                        Price = detail.Price,
                        Discount = detail.Discount,
                        Total = detail.Total
                    });
                }
                
                // Add user shippings
                foreach (var shipping in orderDetails.UserShippings)
                {
                    response.UserShippings.Add(new UserShippingItem
                    {
                        Id = shipping.Id,
                        UserId = shipping.UserId,
                        Address = shipping.Address,
                        PostalCode = shipping.PostalCode,
                        Mobile = shipping.Mobile,
                        City = shipping.City,
                        Province = shipping.Province,
                        IsDefault = shipping.IsDefault
                    });
                }
                
                // Add pay types
                foreach (var payType in orderDetails.PayTypes)
                {
                    response.PayTypes.Add(new PayTypeItem
                    {
                        Id = payType.Id,
                        Name = payType.Name,
                        Description = payType.Description,
                        IsActive = payType.IsActive
                    });
                }
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }

        public override async Task<ChangeOrderDetailResponse> ChangeOrderDetail(ChangeOrderDetailRequest request, ServerCallContext context)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailById(request.DetailId);
                orderDetail.Count = request.Count;
                
                var result = await _mediator.Send(new RefreshOrderDetailQueryReq(orderDetail));
                
                if (result.Status)
                {
                    var orderDetails = await _mediator.Send(new OrderDetailShowQueryReq(orderDetail.OrderId));
                    var response = new OrderDetailsResponse
                    {
                        OrderId = orderDetails.OrderId,
                        UserName = orderDetails.UserName,
                        Total = orderDetails.Total,
                        Discount = orderDetails.Discount,
                        FinalPrice = orderDetails.FinalPrice,
                        ShippingId = orderDetails.ShippingId,
                        ShippingAddress = orderDetails.ShippingAddress,
                        ShippingPostalCode = orderDetails.ShippingPostalCode,
                        ShippingMobile = orderDetails.ShippingMobile
                    };
                    
                    // Add order details
                    foreach (var detail in orderDetails.OrderDetails)
                    {
                        response.Details.Add(new OrderDetailItem
                        {
                            Id = detail.Id,
                            OrderId = detail.OrderId,
                            ProductId = detail.ProductId,
                            ProductName = detail.ProductName,
                            ProductImage = detail.ProductImage,
                            Count = detail.Count,
                            Price = detail.Price,
                            Discount = detail.Discount,
                            Total = detail.Total
                        });
                    }
                    
                    // Add user shippings
                    foreach (var shipping in orderDetails.UserShippings)
                    {
                        response.UserShippings.Add(new UserShippingItem
                        {
                            Id = shipping.Id,
                            UserId = shipping.UserId,
                            Address = shipping.Address,
                            PostalCode = shipping.PostalCode,
                            Mobile = shipping.Mobile,
                            City = shipping.City,
                            Province = shipping.Province,
                            IsDefault = shipping.IsDefault
                        });
                    }
                    
                    // Add pay types
                    foreach (var payType in orderDetails.PayTypes)
                    {
                        response.PayTypes.Add(new PayTypeItem
                        {
                            Id = payType.Id,
                            Name = payType.Name,
                            Description = payType.Description,
                            IsActive = payType.IsActive
                        });
                    }
                    
                    return new ChangeOrderDetailResponse
                    {
                        Status = true,
                        Message = "Order detail updated successfully",
                        Details = response
                    };
                }
                else
                {
                    return new ChangeOrderDetailResponse
                    {
                        Status = false,
                        Message = result.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new ChangeOrderDetailResponse
                {
                    Status = false,
                    Message = $"Error updating order detail: {ex.Message}"
                };
            }
        }

        public override async Task<DeleteOrderDetailResponse> DeleteOrderDetail(DeleteOrderDetailRequest request, ServerCallContext context)
        {
            try
            {
                await _mediator.Send(new OrderDetailDeleteCommandReq(request.DetailId));
                
                return new DeleteOrderDetailResponse
                {
                    Status = true,
                    Message = "Order detail deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new DeleteOrderDetailResponse
                {
                    Status = false,
                    Message = $"Error deleting order detail: {ex.Message}"
                };
            }
        }

        public override async Task<CompleteOrderResponse> CompleteOrder(CompleteOrderRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new CompleteOrderQueryReq(
                    request.OrderId,
                    request.OrderShippingId,
                    request.PayTypeId,
                    request.TransactionId,
                    request.Month));
                
                var response = new CompleteOrderResponse
                {
                    IsSuccessed = result.IsSuccessed,
                    Message = result.Message
                };
                
                if (result.IsSuccessed && result.Object != null)
                {
                    var orderResult = result.Object;
                    response.Result = new CompleteOrderResult
                    {
                        OrderId = orderResult.OrderId,
                        OrderPayId = orderResult.OrderPayId,
                        TrackingCode = orderResult.TrackingCode,
                        Total = orderResult.Total,
                        Url = orderResult.Url,
                        TransactionId = orderResult.TransactionId
                    };
                }
                
                return response;
            }
            catch (Exception ex)
            {
                return new CompleteOrderResponse
                {
                    IsSuccessed = false,
                    Message = $"Error completing order: {ex.Message}"
                };
            }
        }
    }
} 