using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

public record UserShippingOfUserQueryReq(int userId) : IRequest<List<UserShippingDto>>;