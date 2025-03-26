using MediatR;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Commands;

public record OrderDetailDeleteCommandReq(int Id):IRequest<Unit>;