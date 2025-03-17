using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record SendCreditOfUserOrderQueryReq(int orderId) : IRequest<bool>;