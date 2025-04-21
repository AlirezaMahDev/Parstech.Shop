using MediatR;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Requests.Queries
{
    //if user is admin storeId=0
    public record GetChildsAndProductStocksQueryReq(int productId,int storeId):IRequest<ChildsAndStock>;

}
