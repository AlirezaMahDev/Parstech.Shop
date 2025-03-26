using MediatR;
using Parstech.Shop.Context.Application.DTOs.UserStore;

namespace Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

public record StoreListQueryReq() : IRequest<List<UserStoreDto>>;