using MediatR;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record SendCreditOfUserOrderQueryReq(int orderId) :IRequest<bool>;