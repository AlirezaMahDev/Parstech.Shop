using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

public record UserRoleListQueryReq(string userId) : IRequest<List<IUserRoleDto>>;

public record UserRoleCreateQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;

public record UserRoleDeleteQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;