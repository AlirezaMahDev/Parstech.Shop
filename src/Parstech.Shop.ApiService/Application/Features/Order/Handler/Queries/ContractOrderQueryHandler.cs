using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
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
            ContractsDto Result=new ContractsDto();
            Result.Details = new List<ContractDto>();
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
}
