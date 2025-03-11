using MediatR;
using Shop.Application.DTOs.ProductRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRelated.Requests.Commnads
{
    public record CreateProductRelatedCommandReq(ProductRelatedDto productRelatedDto) :IRequest; 

}
