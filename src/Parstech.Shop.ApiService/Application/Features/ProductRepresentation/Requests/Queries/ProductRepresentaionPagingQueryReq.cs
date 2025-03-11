using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRepresentation.Requests.Queries
{
    public record ProductRepresentaionPagingQueryReq(ProductRepresenationParameterDto ProductRepresenationParameterDto) : IRequest<ProductRepresentationPagingDto>;

}
