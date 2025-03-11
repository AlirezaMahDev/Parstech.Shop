using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductRepresentation;

namespace Shop.Application.Features.ProductRepresentation.Requests.Commands
{
    public record ProductRepresesntationCreateCommandReq
        (ProductRepresentationDto ProductRepresentationDto) : IRequest<ProductRepresentationDto>;


    public record ProductRepresesntationQuickCreateCommandReq(ProductRepresentationDto ProductRepresentationDto) : IRequest<ProductRepresentationDto>;
}
