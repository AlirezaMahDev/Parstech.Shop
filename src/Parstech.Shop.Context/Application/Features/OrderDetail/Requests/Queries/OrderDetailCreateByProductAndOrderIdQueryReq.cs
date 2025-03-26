using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record OrderDetailCreateByProductAndOrderIdQueryReq(int orderId,int productId,string userName):IRequest<ResponseDto>;