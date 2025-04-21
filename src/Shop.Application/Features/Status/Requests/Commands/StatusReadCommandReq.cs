using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Status;

namespace Shop.Application.Features.Status.Requests.Commands
{
    public record StatusReadCommandReq() : IRequest<List<StatusDto>>;
}
