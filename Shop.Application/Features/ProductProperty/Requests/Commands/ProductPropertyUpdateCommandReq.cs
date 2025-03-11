using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;

namespace Shop.Application.Features.ProductProperty.Requests.Commands
{
    public record ProductPropertyUpdateCommandReq(ProductPropertyDto ProductPropertyDto) : IRequest<ProductPropertyDto>;

}
