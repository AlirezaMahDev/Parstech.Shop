using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.Order;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class OrderService : Parstech.Shop.Shared.Protos.Order.OrderService.OrderServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<Order> GetOrder(OrderRequest request, ServerCallContext context)
        {
            var order = await _mediator.Send(new OrderReadQueryReq(request.OrderId));
            return _mapper.Map<Order>(order);
        }

        public override async Task<OrderDetailShow> GetOrderDetails(OrderRequest request, ServerCallContext context)
        {
            var orderDetail = await _mediator.Send(new OrderDetailShowQueryReq(request.OrderId));
            return _mapper.Map<OrderDetailShow>(orderDetail);
        }

        public override async Task<OrdersResponse> GetOrdersForUser(UserOrdersRequest request, ServerCallContext context)
        {
            var orders = await _mediator.Send(new OrderListByUserNameQueryReq(request.UserName));
            
            var response = new OrdersResponse();
            foreach (var order in orders)
            {
                response.Orders.Add(_mapper.Map<Order>(order));
            }

            return response;
        }

        public override async Task<OrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            // Implement order creation logic, converting from CreateOrderRequest to your internal commands
            // This is a placeholder - you'll need to adapt based on your actual MediatR command
            var result = await _mediator.Send(new CreateCheckoutQueryReq(request.UserName, request.ProductIds[0]));
            
            return new OrderResponse
            {
                Status = result.IsSuccessed,
                Message = result.Message ?? string.Empty,
                Discount = 0
            };
        }

        public override async Task<OrderResponse> UpdateOrderStatus(UpdateOrderStatusRequest request, ServerCallContext context)
        {
            var success = await _mediator.Send(new UpdateOrderStatusCommandReq(request.OrderId, request.StatusId));
            
            return new OrderResponse
            {
                Status = success,
                Message = success ? "Status updated successfully" : "Failed to update status",
                Discount = 0
            };
        }

        public override async Task<OrderFilter> GetOrderFilters(OrderFiltersRequest request, ServerCallContext context)
        {
            var filters = await _mediator.Send(new OrderFiltersQueryReq());
            return _mapper.Map<OrderFilter>(filters);
        }
    }
}
