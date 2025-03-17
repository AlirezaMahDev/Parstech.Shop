using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserBilling.Requests.Queries;

public record UserBillingOfUserQueryReq(int userId) : IRequest<UserBillingDto>;