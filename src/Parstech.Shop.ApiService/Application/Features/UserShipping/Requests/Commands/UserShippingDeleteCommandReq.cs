using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.UserShipping;

namespace Shop.Application.Features.UserShipping.Requests.Commands
{
    public record UserShippingDeleteCommandReq(int id) : IRequest<Unit>;
}
