using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class ShippingGrpcService : ShippingService.ShippingServiceBase
{
    private readonly IMediator _mediator;

    public ShippingGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<UserShippingResponse> GetFirstUserShipping(FirstShippingRequest request,
        ServerCallContext context)
    {
        try
        {
            var shippingId = await _mediator.Send(new GetFirstUserShippingQueryReq(request.UserId));
            var shipping = await _mediator.Send(new GetUserShippingByIdQueryReq(shippingId));

            return new UserShippingResponse
            {
                ShippingId = shipping.Id,
                UserId = shipping.UserId,
                Address = shipping.Address,
                PostalCode = shipping.PostalCode,
                Mobile = shipping.Mobile,
                City = shipping.City,
                Province = shipping.Province,
                IsDefault = shipping.IsDefault
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<ChangeShippingResponse> ChangeOrderShipping(ChangeShippingRequest request,
        ServerCallContext context)
    {
        try
        {
            var shipping = await _mediator.Send(new OrderShippingChangeQueryReq(
                request.Action,
                request.UserShippingId,
                request.OrderId,
                request.ShippingCost));

            return new ChangeShippingResponse
            {
                Id = shipping.Id,
                OrderId = shipping.OrderId,
                UserShippingId = shipping.UserShippingId,
                Address = shipping.Address,
                PostalCode = shipping.PostalCode,
                Mobile = shipping.Mobile,
                ShippingCost = shipping.ShippingCost
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<OrderShippingResponse> GetOrderShipping(OrderShippingRequest request,
        ServerCallContext context)
    {
        try
        {
            var shipping = await _mediator.Send(new OrderShippingGetByOrderIdQueryReq(request.OrderId));

            return new OrderShippingResponse
            {
                Id = shipping.Id,
                OrderId = shipping.OrderId,
                UserShippingId = shipping.UserShippingId,
                Address = shipping.Address,
                PostalCode = shipping.PostalCode,
                Mobile = shipping.Mobile,
                ShippingCost = shipping.ShippingCost
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}