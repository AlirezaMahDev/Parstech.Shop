using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

public record UserBillingCreateCommandReq(UserBillingDto userBillingDto) : IRequest<UserBillingDto>;