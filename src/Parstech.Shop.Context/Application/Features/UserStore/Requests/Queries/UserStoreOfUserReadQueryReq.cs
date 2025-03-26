using MediatR;
using Parstech.Shop.Context.Application.DTOs.UserStore;

namespace Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

public record UserStoreOfUserReadQueryReq(int userId) : IRequest<UserStoreDto>;