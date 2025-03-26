using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Queries;

public class ProductStockPriceDeleteQueryHandler : IRequestHandler<ProductStockPriceDeleteQueryReq, bool>
{
    private IOrderDetailRepository _orderDetailRep;
    private IProductRepository _productRep;
    private IProductStockPriceRepository _productStockRep;
    private IProductRepresesntationRepository _productRepresentationRep;
    private IProductLogRepository _productLogRep;
    private IMediator _mediator;
    public ProductStockPriceDeleteQueryHandler(IOrderDetailRepository orderDetailRep,
        IProductStockPriceRepository productStockRep,
        IProductRepresesntationRepository productRepresentationRep,
        IProductLogRepository productLogRep,
        IProductRepository productRep,IMediator mediator)
    {
        _productStockRep = productStockRep;
        _orderDetailRep = orderDetailRep;
        _productRepresentationRep = productRepresentationRep;
        _productLogRep = productLogRep;
        _productRep = productRep;
        _mediator = mediator;
    }
    public async Task<bool> Handle(ProductStockPriceDeleteQueryReq request, CancellationToken cancellationToken)
    {
            
        var ExistOrderDetail = await _orderDetailRep.ExistOrderDetailforProductStockPrice(request.productStockPriceId);
        if (ExistOrderDetail)
        {
            return false;
        }
            
        var productStockePrice=await _productStockRep.GetAsync(request.productStockPriceId);
        var productChildList = await _productRep.GetProductsByParrentId(productStockePrice.ProductId);
        var childStockCount = 0;
        foreach(var child in productChildList) 
        {
            var childStocks=await _productStockRep.GetAllByProductId(child.Id);
            if(childStocks.Count > 0)
            {
                childStockCount += childStocks.Count;
            }
        }



        if (childStockCount > 0)
        {
            return false;
        }
        var productRepresentations=await _productRepresentationRep.GetProductRepresentationsWithRepAndProductId(request.repId,request.productStockPriceId);
        foreach(var pr in productRepresentations)
        {
            await _productRepresentationRep.DeleteAsync(pr);
        }

        var productLogs= await _productLogRep.GetProductLogWithProductId(request.productStockPriceId);
        foreach (var pl in productLogs)
        {
            await _productLogRep.DeleteAsync(pl);
        }
        //await _mediator.Send(new RefreshParentQuantityQueryReq(request.productStockPriceId));
            
        await _productStockRep.DeleteAsync(productStockePrice);
            
        return true;
    }
}