using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

public record UserStoreCreateCommandReq(UserStoreDto userStoreDto) : IRequest<UserStoreDto>;