using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record CompleteOrderQueryReq(int orderId,int orderShippingId,int payTypeId,int? transactionId,int? month) :IRequest<ResponseDto>;