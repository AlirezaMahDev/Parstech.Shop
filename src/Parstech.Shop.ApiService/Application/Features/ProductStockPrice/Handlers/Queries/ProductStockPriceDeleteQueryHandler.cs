using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Queries;

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
        IProductRepository productRep,
        IMediator mediator)
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
        bool ExistOrderDetail = await _orderDetailRep.ExistOrderDetailforProductStockPrice(request.productStockPriceId);
        if (ExistOrderDetail)
        {
            return false;
        }

        Domain.Models.ProductStockPrice? productStockePrice =
            await _productStockRep.GetAsync(request.productStockPriceId);
        List<Domain.Models.Product>? productChildList =
            await _productRep.GetProductsByParrentId(productStockePrice.ProductId);
        int childStockCount = 0;
        foreach (Domain.Models.Product? child in productChildList)
        {
            List<Domain.Models.ProductStockPrice>? childStocks = await _productStockRep.GetAllByProductId(child.Id);
            if (childStocks.Count > 0)
            {
                childStockCount += childStocks.Count;
            }
        }


        if (childStockCount > 0)
        {
            return false;
        }

        List<Domain.Models.ProductRepresentation>? productRepresentations =
            await _productRepresentationRep.GetProductRepresentationsWithRepAndProductId(request.repId,
                request.productStockPriceId);
        foreach (Domain.Models.ProductRepresentation? pr in productRepresentations)
        {
            await _productRepresentationRep.DeleteAsync(pr);
        }

        List<Domain.Models.ProductLog>? productLogs =
            await _productLogRep.GetProductLogWithProductId(request.productStockPriceId);
        foreach (Domain.Models.ProductLog? pl in productLogs)
        {
            await _productLogRep.DeleteAsync(pl);
        }
        //await _mediator.Send(new RefreshParentQuantityQueryReq(request.productStockPriceId));

        await _productStockRep.DeleteAsync(productStockePrice);

        return true;
    }
}