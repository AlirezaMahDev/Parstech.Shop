using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Handlers.Commands;

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
        Domain.Models.ProductLog log = new();
        log.CreateDate = DateTime.Now;
        log.ProductStockPriceId = request.productStockPriceId;
        Domain.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        log.UserId = user.Id;
        log.OldValue = request.oldValue;
        log.NewValue = request.newValue;
        log.ProductLogTypeId = request.typeId;
        await _productLogRep.AddAsync(log);
        return Unit.Value;
    }
}