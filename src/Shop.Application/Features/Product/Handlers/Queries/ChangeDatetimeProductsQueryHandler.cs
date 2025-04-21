using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Product.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ChangeDatetimeProductsQueryHandler : IRequestHandler<ChangeDatetimeProductsQueryReq, Unit>
    {
        private readonly IProductRepository _productRep;
        public ChangeDatetimeProductsQueryHandler(IProductRepository productRep)
        {
            _productRep = productRep;   
        }
        public async Task<Unit> Handle(ChangeDatetimeProductsQueryReq request, CancellationToken cancellationToken)
        {
           var list=await _productRep.GetAll();
            foreach (var item in list.OrderByDescending(u=>u.CreateDate))
            {
                item.CreateDate = DateTime.Now;
                await _productRep.UpdateAsync(item);
            }
            return Unit.Value;
        }
    }
}
