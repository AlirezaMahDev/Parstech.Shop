using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.ProductLog.Requests.Commands
{
    public record ProductLogCreateCommandReq(int typeId,string userName,string oldValue,string newValue,int productStockPriceId) : IRequest<Unit>;

}
