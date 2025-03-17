using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record CreateCheckoutQueryReq(string userName, int productId) : IRequest<ResponseDto>;