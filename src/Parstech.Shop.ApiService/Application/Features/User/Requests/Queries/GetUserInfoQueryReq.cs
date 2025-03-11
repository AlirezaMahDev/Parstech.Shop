using MediatR;
using Shop.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Requests.Queries
{
    public record GetUserInfoQueryReq(string userName, string position) : IRequest<UserInfoDto>;
}
