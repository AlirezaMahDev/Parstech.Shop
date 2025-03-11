using MediatR;
using Shop.Application.DTOs.Categury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Representation;

namespace Shop.Application.Features.Representation.Requests.Commands
{
    public record RepresentationReadCommandReq(int repId) : IRequest<RepresentationDto>;
    public record RepresentationReadsCommandReq() : IRequest<List<RepresentationDto>>;
   
}
