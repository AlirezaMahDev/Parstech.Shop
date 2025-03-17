using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

public record UserStoreReadCommandReq(int id) : IRequest<UserStoreDto>;

public record UserStoreReadsCommandReq() : IRequest<List<UserStoreDto>>;