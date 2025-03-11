using MediatR;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRelated.Requests.Queries
{
    public record GetRelatedProductsByProductIdQueryReq(int productId,string userName):IRequest<List<ProductDto>>;
}
