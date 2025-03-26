using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryDeleteCommandReq(int id) : IRequest<int>;