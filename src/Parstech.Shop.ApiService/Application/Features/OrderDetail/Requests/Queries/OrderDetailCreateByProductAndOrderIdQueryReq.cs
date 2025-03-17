using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailCreateByProductAndOrderIdQueryReq(int orderId, int productId, string userName)
    : IRequest<ResponseDto>;