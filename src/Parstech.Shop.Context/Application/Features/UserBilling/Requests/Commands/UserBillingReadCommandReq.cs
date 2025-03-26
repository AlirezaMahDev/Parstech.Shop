using MediatR;
using Parstech.Shop.Context.Application.DTOs.UserBilling;

namespace Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;

public record UserBillingReadCommandReq(int id) : IRequest<UserBillingDto>;