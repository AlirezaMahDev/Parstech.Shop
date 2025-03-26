using MediatR;

using Parstech.Shop.Context.Application.DTOs.UserStore;

namespace Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

public record UserStoreCreateCommandReq(UserStoreDto userStoreDto) : IRequest<UserStoreDto>;