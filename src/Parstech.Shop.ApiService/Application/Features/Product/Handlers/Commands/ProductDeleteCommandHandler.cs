using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.Product.Handlers.Commands
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandReq, Unit>
    {
        public Task<Unit> Handle(ProductDeleteCommandReq request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
