using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;

public record StoreListQueryReq() : IRequest<List<UserStoreDto>>;