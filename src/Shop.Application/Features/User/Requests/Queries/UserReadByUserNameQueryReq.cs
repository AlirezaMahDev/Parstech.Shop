using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.User;

namespace Shop.Application.Features.User.Requests.Queries
{
    public record UserReadByUserNameQueryReq(string userName) : IRequest<UserDto>;
}
