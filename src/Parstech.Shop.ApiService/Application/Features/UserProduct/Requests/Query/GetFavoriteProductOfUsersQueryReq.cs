using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Query;

public record GetFavoriteProductOfUsersQueryReq(string userName) : IRequest<List<FavoriteDto>>;