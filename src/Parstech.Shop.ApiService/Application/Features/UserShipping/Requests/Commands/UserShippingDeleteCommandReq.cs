using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Commands;

public record UserShippingDeleteCommandReq(int id) : IRequest<Unit>;