using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record CountOfOpenOrderFromUserQueryReq(string userName) : IRequest<int>;