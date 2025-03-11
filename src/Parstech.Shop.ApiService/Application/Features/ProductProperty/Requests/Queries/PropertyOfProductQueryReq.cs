using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.ProductProperty;

namespace Shop.Application.Features.ProductProperty.Requests.Queries
{
    public record PropertyOfProductQueryReq(int productId) : IRequest<ProductPropertyDto>;
}
