using MediatR;
using Parstech.Shop.Context.Application.DTOs.UserBilling;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Requests.Queries;

public record UserBillingOfUserQueryReq(int userId):IRequest<UserBillingDto>;