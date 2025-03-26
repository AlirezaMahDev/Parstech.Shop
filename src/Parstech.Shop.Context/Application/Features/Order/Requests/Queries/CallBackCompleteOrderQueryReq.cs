using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record CallBackCompleteOrderQueryReq(int orderId, int transactionId,string TrackingCode) : IRequest<ResponseDto>;