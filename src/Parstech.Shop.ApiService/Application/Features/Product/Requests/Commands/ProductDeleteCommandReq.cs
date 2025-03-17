using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;

public record ProductDeleteCommandReq(int id) : IRequest<Unit>;