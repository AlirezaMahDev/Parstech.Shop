using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

public record UserBillingDeleteCommandReq(int id) : IRequest<Unit>;