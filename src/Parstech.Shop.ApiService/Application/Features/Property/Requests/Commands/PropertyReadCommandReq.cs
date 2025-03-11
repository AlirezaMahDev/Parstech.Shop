using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Property;

namespace Shop.Application.Features.Property.Requests.Commands
{
    public record PropertyReadCommandReq(int id) : IRequest<PropertyDto>;

}
