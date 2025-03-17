using AutoMapper;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Command;
using Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Services;

public class OrderGrpcService : Shop.Shared.Protos.Order.OrderService.OrderServiceBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderGrpcService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<Order> GetOrder(OrderRequest request, ServerCallContext context)
    {
        var order = await _mediator.Send(new GetOrderDetailByIdQueryReq(request.OrderId));
        return MapToOrderProto(order);
    }

    public override async Task<OrderDetailShow> GetOrderDetails(OrderRequest request, ServerCallContext context)
    {
        var orderDetail = await _mediator.Send(new OrderDetailShowQueryReq(request.OrderId));
        return MapToOrderDetailShowProto(orderDetail);
    }

    public override async Task<OrdersResponse> GetOrdersForUser(UserOrdersRequest request, ServerCallContext context)
    {
        // Implement this method based on your requirements
        return new OrdersResponse();
    }

    public override async Task<OrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
    {
        // Implement this method based on your requirements
        return new();
    }

    public override async Task<OrderResponse> UpdateOrderStatus(UpdateOrderStatusRequest request,
        ServerCallContext context)
    {
        // Implement this method based on your requirements
        return new();
    }

    public override async Task<OrderFilter> GetOrderFilters(OrderFiltersRequest request, ServerCallContext context)
    {
        var filters = await _mediator.Send(new OrdersFilterDataQueryReq(request.StoreName));
        return MapToOrderFilterProto(filters);
    }

    public override async Task<PagingDto> GetOrdersPaging(ParameterDto request, ServerCallContext context)
    {
        var parameter = new Parstech.Shop.Application.DTOs.Paging.ParameterDto
        {
            CurrentPage = request.CurrentPage,
            TakePage = request.TakePage,
            SearchKey = request.SearchKey,
            StatusKey = request.StatusKey,
            PayTypeKey = request.PayTypeKey,
            StoreKey = request.StoreKey,
            CodeKey = request.CodeKey,
            CustomerKey = request.CustomerKey,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            store = request.Store
        };

        var pagingResult = await _mediator.Send(new OrderPagingQueryReq(parameter));
        return MapToPagingDtoProto(pagingResult);
    }

    public override async Task<OrderStatusesResponse> GetOrderStatuses(OrderRequest request, ServerCallContext context)
    {
        var statuses = await _mediator.Send(new GetOrderStatusByOrderIdQueryReq(request.OrderId));
        var response = new OrderStatusesResponse();

        foreach (var status in statuses)
        {
            response.Statuses.Add(MapToOrderStatusDtoProto(status));
        }

        return response;
    }

    public override async Task<OrderResponse> CreateOrderStatus(OrderStatusRequest request, ServerCallContext context)
    {
        try
        {
            var orderStatusDto = new Parstech.Shop.Shared.DTOsStatus.OrderStatusDto
            {
                Id = request.OrderStatus.Id,
                OrderId = request.OrderStatus.OrderId,
                StatusId = request.OrderStatus.StatusId,
                Description = request.OrderStatus.Description,
                CreateBy = request.OrderStatus.CreateBy,
                CreateDate = request.OrderStatus.CreateDate.ToDateTime()
            };

            if (request.FileData != null && request.FileData.Length > 0)
            {
                // Create a temporary file to pass to MediatR
                string tempFilePath = Path.GetTempFileName();
                await File.WriteAllBytesAsync(tempFilePath, request.FileData.ToByteArray());

                // Create FormFile from the temporary file
                using FileStream stream = new(tempFilePath, FileMode.Open);
                FormFile file = new(stream, 0, stream.Length, null, Path.GetFileName(tempFilePath))
                {
                    Headers = new HeaderDictionary(), ContentType = "application/octet-stream"
                };

                // Set file in DTO
                orderStatusDto.File = file;
            }

            var response = await _mediator.Send(new OrderStatusCreatCommandReq(orderStatusDto));
            return MapToOrderResponseProto(response);
        }
        catch (Exception ex)
        {
            return new()
            {
                Status = false, Message = $"Error creating order status: {ex.Message}", IsSucceeded = false
            };
        }
    }

    public override async Task<OrderResponse> ChangeOrderShipping(OrderShippingChangeRequest request,
        ServerCallContext context)
    {
        var orderId = await _mediator.Send(new OrderShippingChangeQueryReq(
            request.Type,
            request.UserShippingId,
            request.OrderId,
            request.OrderShippingId));

        return new() { Status = true, Object = new StringValue { Value = orderId.ToString() }, IsSucceeded = true };
    }

    public override async Task<OrderWordFileResponse> GenerateOrderWordFile(OrderRequest request,
        ServerCallContext context)
    {
        var orderDetailDto = await _mediator.Send(new OrderDetailShowQueryReq(request.OrderId));
        var wordFilePath = await _mediator.Send(new OrderWordFileQueryReq(orderDetailDto));

        return new OrderWordFileResponse { FilePath = wordFilePath };
    }

    public override async Task<OrderResponse> CompleteOrderByAdmin(CompleteOrderRequest request,
        ServerCallContext context)
    {
        var response = await _mediator.Send(new CompleteOrderByAdminQueryReq(
            request.OrderId,
            request.TypeName,
            request.Month?.Value));

        return MapToOrderResponseProto(response);
    }

    public override async Task<OrderPaysResponse> GetOrderPays(OrderRequest request, ServerCallContext context)
    {
        var orderPays = await _mediator.Send(new OrderPaysOfOrderQueryReq(request.OrderId));
        var response = new OrderPaysResponse();

        foreach (var pay in orderPays)
        {
            response.Payments.Add(MapToOrderPayDtoProto(pay));
        }

        return response;
    }

    public override async Task<OrderResponse> AddOrderPay(OrderPayRequest request, ServerCallContext context)
    {
        var orderPayDto = new Parstech.Shop.Shared.DTOsPay.OrderPayDto
        {
            Id = request.OrderPay.Id,
            OrderId = request.OrderPay.OrderId,
            PayTypeId = request.OrderPay.PayTypeId,
            Amount = request.OrderPay.Amount,
            RefId = request.OrderPay.RefId,
            Description = request.OrderPay.Description,
            CreateBy = request.OrderPay.CreateBy,
            CreateDate = request.OrderPay.CreateDate.ToDateTime()
        };

        var response = await _mediator.Send(new OrderPayCreateCommandReq(orderPayDto));
        return MapToOrderResponseProto(response);
    }

    public override async Task<OrderResponse> DeleteOrderPay(OrderPayDeleteRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(new OrderPayDeleteCommandReq(request.Id));
        return MapToOrderResponseProto(response);
    }

    #region Mapping Methods

    private Order MapToOrderProto(OrderDto orderDto)
    {
        if (orderDto == null)
        {
            return null;
        }

        var order = new Order
        {
            OrderId = orderDto.OrderId,
            UserId = orderDto.UserId,
            UserName = orderDto.UserName ?? string.Empty,
            Costumer = orderDto.Costumer ?? string.Empty,
            FirstName = orderDto.FirstName ?? string.Empty,
            LastName = orderDto.LastName ?? string.Empty,
            OrderCode = orderDto.OrderCode ?? string.Empty,
            OrderSum = orderDto.OrderSum,
            Shipping = orderDto.Shipping,
            Tax = orderDto.Tax,
            Discount = orderDto.Discount,
            Total = orderDto.Total,
            IsFinaly = orderDto.IsFinaly,
            IntroCoin = orderDto.IntroCoin,
            IsDelete = orderDto.IsDelete,
            TaxId = orderDto.TaxId,
            Status = orderDto.Status ?? string.Empty,
            StatusIcon = orderDto.StatusIcon ?? string.Empty,
            PayType = orderDto.PayType ?? string.Empty,
            TypeName = orderDto.TypeName ?? string.Empty,
            StatusName = orderDto.StatusName ?? string.Empty
        };

        if (orderDto.CreateDate.HasValue)
        {
            order.CreateDate = Timestamp.FromDateTime(orderDto.CreateDate.Value.ToUniversalTime());
        }

        if (!string.IsNullOrEmpty(orderDto.CreateDateShamsi))
        {
            order.CreateDateShamsi = orderDto.CreateDateShamsi;
        }

        if (!string.IsNullOrEmpty(orderDto.IntroCode))
        {
            order.IntroCode = new StringValue { Value = orderDto.IntroCode };
        }

        if (orderDto.ConfirmPayment.HasValue)
        {
            order.ConfirmPayment = new BoolValue { Value = orderDto.ConfirmPayment.Value };
        }

        if (!string.IsNullOrEmpty(orderDto.FactorFile))
        {
            order.FactorFile = new StringValue { Value = orderDto.FactorFile };
        }

        return order;
    }

    private OrderDetailShow MapToOrderDetailShowProto(Shop.Application.DTOs.OrderDetail.OrderDetailShowDto dto)
    {
        // Implementation based on your specific requirements
        var result = new OrderDetailShow();

        if (dto.Order != null)
        {
            result.Order = MapToOrderProto(dto.Order);
        }

        // Map other properties as needed

        return result;
    }

    private OrderFilter MapToOrderFilterProto(Shop.Application.DTOs.Order.OrderFilterDto dto)
    {
        var filter = new OrderFilter();

        if (dto.Stores != null)
        {
            foreach (var store in dto.Stores)
            {
                filter.Stores.Add(new StoreFilter
                {
                    StoreName = store.StoreName ?? string.Empty,
                    UserStoreId = store.UserStoreId,
                    UserId = store.UserId
                });
            }
        }

        // Map other collections

        return filter;
    }

    private PagingDto MapToPagingDtoProto(PagingDto dto)
    {
        PagingDto pagingDto = new()
        {
            TotalCount = dto.TotalCount,
            PageCount = dto.PageCount,
            CurrentPage = dto.CurrentPage,
            TakePage = dto.TakePage
        };

        if (dto.Items != null)
        {
            foreach (var item in dto.Items)
            {
                pagingDto.Items.Add(MapToOrderProto(item));
            }
        }

        return pagingDto;
    }

    private OrderPayDto MapToOrderPayDtoProto(OrderPayDto dto)
    {
        OrderPayDto orderPayDto = new()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            PayTypeId = dto.PayTypeId,
            Amount = dto.Amount,
            RefId = dto.RefId ?? string.Empty,
            Description = dto.Description ?? string.Empty,
            CreateBy = dto.CreateBy ?? string.Empty
        };

        if (dto.CreateDate.HasValue)
        {
            orderPayDto.CreateDate = Timestamp.FromDateTime(dto.CreateDate.Value.ToUniversalTime());
        }

        return orderPayDto;
    }

    private OrderStatusDto MapToOrderStatusDtoProto(OrderStatusDto dto)
    {
        OrderStatusDto orderStatusDto = new()
        {
            Id = dto.Id,
            OrderId = dto.OrderId,
            StatusId = dto.StatusId,
            Description = dto.Description ?? string.Empty,
            CreateBy = dto.CreateBy ?? string.Empty
        };

        if (dto.CreateDate.HasValue)
        {
            orderStatusDto.CreateDate = Timestamp.FromDateTime(dto.CreateDate.Value.ToUniversalTime());
        }

        return orderStatusDto;
    }

    private OrderResponse MapToOrderResponseProto(ResponseDto dto)
    {
        OrderResponse response = new()
        {
            Status = dto.IsSuccessed, Message = dto.Message ?? string.Empty, IsSucceeded = dto.IsSuccessed
        };

        if (dto.Object != null)
        {
            response.Object = new StringValue { Value = dto.Object.ToString() };
        }

        return response;
    }

    #endregion
}