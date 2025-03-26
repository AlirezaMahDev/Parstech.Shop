using MediatR;

namespace Parstech.Shop.Context.Application.Features.IRole.Requests.Commands;

public record IRoleDeleteCommandReq(string id) : IRequest<Unit>;