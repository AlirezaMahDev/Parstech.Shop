using MediatR;

namespace Parstech.Shop.Context.Application.Features.OrderShipping.Request.Queries;

//Type=Refresh Or Change
public record OrderShippingChangeQueryReq(string Type,int UserShippingId,int OrderId, long OrderSum) : IRequest<long>;