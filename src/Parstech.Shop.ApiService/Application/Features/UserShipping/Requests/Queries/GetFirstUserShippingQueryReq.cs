using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

public record GetFirstUserShippingQueryReq(int userId) : IRequest<int>;