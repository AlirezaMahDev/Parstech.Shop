using MediatR;
using Shop.Application.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Property.Requests.Commands
{
    public record AllPropertiesReadCommandReq : IRequest<List<PropertyDto>>;
}
