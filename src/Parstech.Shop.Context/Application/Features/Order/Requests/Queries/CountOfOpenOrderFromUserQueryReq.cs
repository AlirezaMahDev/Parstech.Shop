using MediatR;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

public record CountOfOpenOrderFromUserQueryReq(string userName):IRequest<int>;