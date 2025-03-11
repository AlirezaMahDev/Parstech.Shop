using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductLog.Requests.Commands;

namespace Shop.Application.Features.ProductLog.Handlers.Commands
{
    public class ProductLogCreateCommandHandler : IRequestHandler<ProductLogCreateCommandReq, Unit>
    {
        private readonly IProductLogRepository _productLogRep;
        private readonly IUserRepository _userRep;

        public ProductLogCreateCommandHandler(IProductLogRepository productLogRep, IUserRepository userRep)
        {
            _productLogRep = productLogRep;
            _userRep = userRep;
        }
        public async Task<Unit> Handle(ProductLogCreateCommandReq request, CancellationToken cancellationToken)
        {
            Domain.Models.ProductLog log = new Domain.Models.ProductLog();
            log.CreateDate=DateTime.Now;
            log.ProductStockPriceId = request.productStockPriceId;
            var user =await _userRep.GetUserByUserName(request.userName);
            log.UserId = user.Id;
            log.OldValue = request.oldValue;
            log.NewValue = request.newValue;
            log.ProductLogTypeId = request.typeId;
            await _productLogRep.AddAsync(log);
            return Unit.Value;
        }
    }
}
