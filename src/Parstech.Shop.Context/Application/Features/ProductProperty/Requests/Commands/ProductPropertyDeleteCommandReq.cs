using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyDeleteCommandReq(int id) : IRequest<Unit>;