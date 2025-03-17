using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;

public record UserDeleteCommandReq(int id) : IRequest<Unit>;