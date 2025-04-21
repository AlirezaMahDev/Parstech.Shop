using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.IRole;
using Shop.Domain.Models;

namespace Shop.Application.Features.IRole.Requests.Commands
{
    public record IRoleCreateCommandReq(IRoleDto roleDto) : IRequest<Irole>;
}
