using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class OrderCreateByUserIdQueryHandler : IRequestHandler<OrderCreateByUserIdQueryReq, OrderDto>
{
    private IOrderRepository _orderRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderCreateByUserIdQueryHandler(IOrderRepository orderRep, IMapper mapper, IMediator mediator)
    {
        _orderRep = orderRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderDto> Handle(OrderCreateByUserIdQueryReq request, CancellationToken cancellationToken)
    {
        OrderDto item = new();
        //item.CreateDate = DateTime.Now.ToShamsi();
        item.UserId = request.userId;
        item.OrderCode = CodeGenerator.GenerateOrderCode();
        item.OrderSum = 0;
        item.Tax = 0;
        item.Discount = 0;
        item.Shipping = 0;
        item.Total = 0;
        item.IsFinaly = false;
        item.IntroCoin = 0;
        item.ConfirmPayment = false;
        item.IsDelete = false;
        item.TaxId = 1;
        Shared.Models.Order? order = _mapper.Map<Shared.Models.Order>(item);
        order.CreateDate = DateTime.Now;
        Shared.Models.Order orderResult = await _orderRep.AddAsync(order);

        return _mapper.Map<OrderDto>(orderResult);
    }
}