using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;

public record IRoleDeleteCommandReq(string id) : IRequest<Unit>;