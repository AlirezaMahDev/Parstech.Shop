using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.User;

namespace Shop.Application.Features.Product.Requests.Commands
{
    public record ProductCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;
    public record ProductWordpressCreateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;
}
