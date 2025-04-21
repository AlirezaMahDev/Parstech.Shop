using MediatR;
using Shop.Application.DTOs.ProductProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductProperty.Requests.Queries
{
    public record BasePropertiesOfProductQueryReq(int productId):IRequest<List<BaseProductPropertyDto>>;
}
