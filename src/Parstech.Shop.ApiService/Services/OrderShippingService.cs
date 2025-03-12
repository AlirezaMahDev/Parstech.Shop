using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.OrderShipping;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.Features.OrderShipping.Handlers.Commands;
using Shop.Application.Features.OrderShipping.Handlers.Queries;
using Shop.Application.Features.OrderShipping.Requests.Commands;
using Shop.Application.Features.OrderShipping.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class OrderShippingService : OrderShippingService.OrderShippingServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderShippingService> _logger;

        public OrderShippingService(IMediator mediator, IMapper mapper, ILogger<OrderShippingService> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<OrderShipping> GetOrderShipping(OrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                var orderShippingDto = await _mediator.Send(new OrderShippingReadCommandReq(request.ShippingId));
                return _mapper.Map<OrderShipping>(orderShippingDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order shipping with ID {ShippingId}", request.ShippingId);
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order shipping: {ex.Message}"));
            }
        }

        public override async Task<OrderShippingListResponse> GetOrderShippingsByOrderId(OrderShippingsByOrderRequest request, ServerCallContext context)
        {
            try
            {
                var orderShippingList = await _mediator.Send(new OrderShippingListQueryReq(request.OrderId));
                var response = new OrderShippingListResponse();
                response.ShippingItems.AddRange(_mapper.Map<IEnumerable<OrderShipping>>(orderShippingList));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order shippings for order {OrderId}", request.OrderId);
                throw new RpcException(new Status(StatusCode.Internal, $"Error getting order shippings: {ex.Message}"));
            }
        }

        public override async Task<OrderShipping> CreateOrderShipping(CreateOrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                // Create the OrderShippingDto from request
                var orderShippingDto = new OrderShippingDto
                {
                    OrderId = request.OrderId,
                    ShippingTypeId = request.ShippingTypeId,
                    ShippingType = request.ShippingType,
                    FirstName = request.FirstName?.Value,
                    LastName = request.LastName?.Value,
                    Phone = request.Phone?.Value,
                    Mobile = request.Mobile?.Value,
                    PostCode = request.PostCode?.Value,
                    FullAddress = request.FullAddress?.Value
                };

                // Call the mediator to create the order shipping
                var result = await _mediator.Send(new OrderShippingCreateCommandReq(orderShippingDto));
                
                // Map the result to the response
                return _mapper.Map<OrderShipping>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order shipping for order {OrderId}", request.OrderId);
                throw new RpcException(new Status(StatusCode.Internal, $"Error creating order shipping: {ex.Message}"));
            }
        }

        public override async Task<OrderShipping> UpdateOrderShipping(UpdateOrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                // Create DTO from request
                var orderShippingDto = new OrderShippingDto
                {
                    Id = request.Id,
                    OrderId = request.OrderId,
                    ShippingTypeId = request.ShippingTypeId,
                    ShippingType = request.ShippingType,
                    FirstName = request.FirstName?.Value,
                    LastName = request.LastName?.Value,
                    Phone = request.Phone?.Value,
                    Mobile = request.Mobile?.Value,
                    PostCode = request.PostCode?.Value,
                    FullAddress = request.FullAddress?.Value
                };

                // Call the mediator to update the order shipping
                var result = await _mediator.Send(new OrderShippingUpdateCommandReq(orderShippingDto));
                
                // Map the result to the response
                return _mapper.Map<OrderShipping>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order shipping with ID {ShippingId}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, $"Error updating order shipping: {ex.Message}"));
            }
        }

        public override async Task<OrderShippingResponse> DeleteOrderShipping(OrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                // Call the mediator to delete the order shipping
                await _mediator.Send(new OrderShippingDeleteCommandReq(request.ShippingId));
                
                return new OrderShippingResponse
                {
                    Status = true,
                    Message = "Order shipping deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting order shipping with ID {ShippingId}", request.ShippingId);
                return new OrderShippingResponse
                {
                    Status = false,
                    Message = $"Error deleting order shipping: {ex.Message}"
                };
            }
        }

        public override async Task<OrderShippingResponse> ChangeOrderShipping(ChangeOrderShippingRequest request, ServerCallContext context)
        {
            try
            {
                // Convert the proto UserShipping list to DTOs
                var userShippingDtos = _mapper.Map<List<Shop.Application.DTOs.UserShipping.UserShippingDto>>(request.UserShippings);
                
                // Create the change DTO
                var changeDto = new OrderShippingChangeDto
                {
                    OrderShippingId = request.OrderShippingId,
                    UserShippings = userShippingDtos
                };

                // Call the mediator to change the order shipping
                await _mediator.Send(new OrderShippingChangeCommandReq(changeDto));
                
                return new OrderShippingResponse
                {
                    Status = true,
                    Message = "Order shipping changed successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing order shipping with ID {ShippingId}", request.OrderShippingId);
                return new OrderShippingResponse
                {
                    Status = false,
                    Message = $"Error changing order shipping: {ex.Message}"
                };
            }
        }
    }
}
