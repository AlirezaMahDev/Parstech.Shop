using MediatR;
using Shop.Application.DTOs.UserProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserProduct.Requests.Command
{
    public record CreateUserProductCommandReq(string userName,int productId,string type):IRequest<bool>;
}
