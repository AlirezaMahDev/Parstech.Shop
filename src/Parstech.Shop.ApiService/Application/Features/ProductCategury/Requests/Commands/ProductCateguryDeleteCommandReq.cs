using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryDeleteCommandReq(int id) : IRequest<int>;