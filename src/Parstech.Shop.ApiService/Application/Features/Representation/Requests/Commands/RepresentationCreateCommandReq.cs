using MediatR;
using Shop.Application.DTOs.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Representation.Requests.Commands
{
    public record RepresentationCreateCommandReq(RepresentationDto RepresentationDto) :IRequest<RepresentationDto>;

}
