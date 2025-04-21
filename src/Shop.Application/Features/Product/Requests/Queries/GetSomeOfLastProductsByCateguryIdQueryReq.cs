using MediatR;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
    public record GetSomeOfLastProductsByCateguryIdQueryReq(int Take,int CateguryId):IRequest<List<ProductListShowDto>>;
}
