using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.User;

namespace Shop.Application.Features.User.Requests.Commands
{
    public record RahkaranUserUpdateCommandReq(RahkaranUserDto dto) : IRequest<RahkaranUserDto>;
}
