using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;

public record UserSaleReadByLatinNameQueryReq(string latinName) : IRequest<UserStoreDto>;