using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.RepresentationType;

namespace Shop.Application.Features.RepresentationType.Requests.Commands
{
    public record RepresentationTypeReadCommandReq(int RepTypeId) : IRequest<RepresentationTypeDto>;
    public record RepresentationTypeReadsCommandReq() : IRequest<List<RepresentationTypeDto>>;

}
