using MediatR;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Commands;

public record UserDeleteCommandReq(int id) : IRequest<Unit>;