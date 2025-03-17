using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class ContractOrderQueryHandler : IRequestHandler<ContractOrderQueryReq, ContractsDto>
{
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IOrderRepository _orderRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IMediator _mediator;

    public ContractOrderQueryHandler(IProductStockPriceRepository productStockPriceRep,
        IOrderDetailRepository orderDetailRep,
        IUserStoreRepository userStoreRep,
        IOrderRepository orderRep,
        IMediator mediator)
    {
        _orderDetailRep = orderDetailRep;
        _productStockPriceRep = productStockPriceRep;
        _userStoreRep = userStoreRep;
        _orderRep = orderRep;
        _mediator = mediator;
    }

    public async Task<ContractsDto> Handle(ContractOrderQueryReq request, CancellationToken cancellationToken)
    {
        ContractsDto result = new();
        result.Details = new();
        result.TotalStore = 0;
        result.TotalWe = 0;
        Shared.Models.Order? order = await _orderRep.GetAsync(request.orderId);
        List<Shared.Models.OrderDetail> details = await _orderDetailRep.GetOrderDetailsByOrderId(order.OrderId);
        foreach (Shared.Models.OrderDetail item in details)
        {
            var con = await _mediator.Send(new ContractOrderDetailQueryReq(item, request.Store));
            result.Details.Add(con);
            result.TotalStore += con.Store;
            result.TotalWe += con.We;
        }

        return result;
    }
}