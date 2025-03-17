using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;

public record OrderDeleteCommandReq(int OrderId) : IRequest<Unit>;