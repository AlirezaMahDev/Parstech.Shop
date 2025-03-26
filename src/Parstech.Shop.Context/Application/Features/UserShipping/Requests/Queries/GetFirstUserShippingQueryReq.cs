using MediatR;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

public record GetFirstUserShippingQueryReq(int userId):IRequest<int>;