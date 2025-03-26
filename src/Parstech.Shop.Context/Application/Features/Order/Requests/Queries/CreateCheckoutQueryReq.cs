using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record CreateCheckoutQueryReq(string userName, int productId) : IRequest<ResponseDto>;