using MediatR;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
    public record ProductPagingForAdminQueryReq(ProductParameterDto ProductParameterDto) : IRequest<ProductPageingDto>;
    
}
