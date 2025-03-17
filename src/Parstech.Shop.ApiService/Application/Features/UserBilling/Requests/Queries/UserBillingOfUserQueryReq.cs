using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Queries;

public record UserBillingOfUserQueryReq(int userId) : IRequest<UserBillingDto>;