using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Product;

namespace Shop.Application.Features.Product.Requests.Queries
{
    public record ProductEditContentQueryReq(int productId, string content) : IRequest<ProductDto>;

}
