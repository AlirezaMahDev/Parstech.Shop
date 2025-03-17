using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record CallBackCompleteOrderQueryReq(int orderId, int transactionId, string TrackingCode)
    : IRequest<ResponseDto>;