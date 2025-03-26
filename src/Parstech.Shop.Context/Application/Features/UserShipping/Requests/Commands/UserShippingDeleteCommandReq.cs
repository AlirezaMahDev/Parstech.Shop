using MediatR;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;

public record UserShippingDeleteCommandReq(int id) : IRequest<Unit>;