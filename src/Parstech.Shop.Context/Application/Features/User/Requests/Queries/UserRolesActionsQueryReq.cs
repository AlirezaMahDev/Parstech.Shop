using MediatR;

using Parstech.Shop.Context.Application.DTOs.IUserRole;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record UserRoleListQueryReq(string userId) : IRequest<List<IUserRoleDto>>;
public record UserRoleCreateQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;
public record UserRoleDeleteQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;