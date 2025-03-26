using MediatR;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;

public record UserBillingDeleteCommandReq(int id) : IRequest<Unit>;