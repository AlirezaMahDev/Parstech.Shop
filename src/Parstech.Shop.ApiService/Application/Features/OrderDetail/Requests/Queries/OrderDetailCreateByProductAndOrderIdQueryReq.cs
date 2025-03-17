using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailCreateByProductAndOrderIdQueryReq(int orderId, int productId, string userName)
    : IRequest<ResponseDto>;