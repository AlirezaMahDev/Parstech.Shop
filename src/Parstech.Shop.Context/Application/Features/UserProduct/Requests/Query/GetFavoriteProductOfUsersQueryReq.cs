using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductProperty;

namespace Parstech.Shop.Context.Application.Features.UserProduct.Requests.Query;

public record GetFavoriteProductOfUsersQueryReq(string userName):IRequest<List<FavoriteDto>>;