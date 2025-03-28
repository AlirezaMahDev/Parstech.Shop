﻿using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

public class OrderWordFileQueryHandler : IRequestHandler<OrderWordFileQueryReq, string>
{
    private readonly IOrderRepository _orderRepository;

    public OrderWordFileQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public async Task<string> Handle(OrderWordFileQueryReq request, CancellationToken cancellationToken)
    {
        return _orderRepository.GenerateWordOfOrder(request.OrderDetailShowDto);
    }
}