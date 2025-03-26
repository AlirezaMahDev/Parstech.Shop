using MediatR;

namespace Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

public record OrderDeleteCommandReq(int OrderId) : IRequest<Unit>;