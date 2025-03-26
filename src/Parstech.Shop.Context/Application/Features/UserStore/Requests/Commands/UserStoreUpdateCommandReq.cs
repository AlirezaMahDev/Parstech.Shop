using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserStore;

namespace Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

public record UserStoreUpdateCommandReq(UserStoreDto userStoreDto) : IRequest<UserStoreDto>;