using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

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
        IOrderRepository orderRep, IMediator mediator)
    {
        _orderDetailRep = orderDetailRep;
        _productStockPriceRep = productStockPriceRep;
        _userStoreRep = userStoreRep;
        _orderRep = orderRep;
        _mediator = mediator;
    }
    public async Task<ContractsDto> Handle(ContractOrderQueryReq request, CancellationToken cancellationToken)
    {
        ContractsDto Result=new();
        Result.Details = new();
        Result.TotalStore = 0;
        Result.TotalWe = 0;
        var order =await _orderRep.GetAsync(request.orderId);
        var details =await _orderDetailRep.GetOrderDetailsByOrderId(order.OrderId);
        foreach (var item in details)
        {
            var con =await _mediator.Send(new ContractOrderDetailQueryReq(item,request.Store));
            Result.Details.Add(con);
            Result.TotalStore += con.Store;
            Result.TotalWe += con.We;
        }
        return Result;
    }
}