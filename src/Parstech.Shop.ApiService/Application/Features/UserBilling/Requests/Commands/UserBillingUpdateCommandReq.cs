using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

public record UserBillingUpdateCommandReq(UserBillingDto userBillingDto) : IRequest<UserBillingDto>;