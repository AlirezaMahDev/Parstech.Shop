using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.IUserRole;

namespace Shop.Application.Features.User.Requests.Queries
{
    public record UserRoleListQueryReq(string userId) : IRequest<List<IUserRoleDto>>;
    public record UserRoleCreateQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;
    public record UserRoleDeleteQueryReq(IUserRoleDto IUserRoleDto) : IRequest<Unit>;
}
