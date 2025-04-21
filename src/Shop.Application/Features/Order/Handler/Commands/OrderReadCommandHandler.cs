using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Order.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Commands
{
    public class OrderReadCommandHandler : IRequestHandler<OrderReadCommandReq, OrderDto>
    {
        //private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private IMediator _mediator;
        private readonly string _connectionString;

        public OrderReadCommandHandler(IConfiguration configuration, IMapper mapper, IMediator mediator)
        {
            //_orderRepository = orderRepository;
            _mapper = mapper;
            _mediator = mediator;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<OrderDto> Handle(OrderReadCommandReq request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * FROM Orders where OrderId={request.id};";
            var order = DapperHelper.ExecuteCommand<OrderDto>(_connectionString, conn => conn.Query<OrderDto>(query).FirstOrDefault());
            //var order = await _orderRepository.GetAsync(request.id);
            //return _mapper.Map<OrderDto>(order);
            return order;
        }
    }
}
