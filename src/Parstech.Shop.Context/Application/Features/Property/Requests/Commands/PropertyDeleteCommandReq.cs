using MediatR;

namespace Parstech.Shop.Context.Application.Features.Property.Requests.Commands;

public record PropertyDeleteCommandReq(int id) : IRequest<Unit>;