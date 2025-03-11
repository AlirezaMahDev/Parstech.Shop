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
    public record ProductUpdateCommandReq(ProductDto ProductDto) : IRequest<ProductDto>;
}
