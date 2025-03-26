using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserShipping;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

public record UserShippingOfUserQueryReq(int userId) : IRequest<List<UserShippingDto>>;