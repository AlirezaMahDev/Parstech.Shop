using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

public record UserShippingOfUserQueryReq(int userId) : IRequest<List<UserShippingDto>>;