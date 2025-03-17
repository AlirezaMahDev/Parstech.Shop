using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;

public record ProductPropertyDeleteCommandReq(int id) : IRequest<Unit>;