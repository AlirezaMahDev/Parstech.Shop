using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Commands
{
    public class ProductStockPriceDeleteCommandHandler : IRequestHandler<ProductStockPriceDeleteCommandReq, Unit>
    {
        public Task<Unit> Handle(ProductStockPriceDeleteCommandReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
