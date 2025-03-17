using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Commands;

public record OrderDetailDeleteCommandReq(int Id) : IRequest<Unit>;