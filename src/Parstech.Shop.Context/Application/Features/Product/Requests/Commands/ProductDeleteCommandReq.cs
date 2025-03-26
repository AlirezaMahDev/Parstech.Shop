using MediatR;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Commands;

public record ProductDeleteCommandReq(int id) : IRequest<Unit>;