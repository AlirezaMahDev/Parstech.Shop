using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Commands;

public record UserBillingReadCommandReq(int id) : IRequest<UserBillingDto>;