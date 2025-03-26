using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserStore;

namespace Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

public record UserStoreReadCommandReq(int id) : IRequest<UserStoreDto>;
public record UserStoreReadsCommandReq() : IRequest<List<UserStoreDto>>;