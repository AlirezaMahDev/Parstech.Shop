using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;

public record UserStoreOfUserReadQueryReq(int userId) : IRequest<UserStoreDto>;