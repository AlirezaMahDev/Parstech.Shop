using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

public record UserStoreUpdateCommandReq(UserStoreDto userStoreDto) : IRequest<UserStoreDto>;