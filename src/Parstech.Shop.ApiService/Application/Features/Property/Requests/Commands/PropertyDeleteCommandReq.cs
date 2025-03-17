using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

public record PropertyDeleteCommandReq(int id) : IRequest<Unit>;