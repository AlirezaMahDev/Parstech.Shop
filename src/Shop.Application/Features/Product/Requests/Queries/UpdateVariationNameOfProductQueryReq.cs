using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
    public record UpdateVariationNameOfProductQueryReq(int productId,string VariationName):IRequest<bool>;
    
}
