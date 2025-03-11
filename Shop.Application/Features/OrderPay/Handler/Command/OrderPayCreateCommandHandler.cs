using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.OrderPay.Request.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Handler.Command
{
	public class OrderPayCreateCommandHandler : IRequestHandler<OrderPayCreateCommandReq, ResponseDto>
	{
		private readonly IOrderPayRepository _orderPayRep;
		private readonly IMapper _mapper;
        public OrderPayCreateCommandHandler(IOrderPayRepository orderPayRep, IMapper mapper)
        {
            _orderPayRep = orderPayRep;
			_mapper= mapper;
        }
        public async Task<ResponseDto> Handle(OrderPayCreateCommandReq request, CancellationToken cancellationToken)
		{
			ResponseDto response = new ResponseDto();
			var item = _mapper.Map<Domain.Models.OrderPay>(request.orderPayDto);
			Random random=new Random();
			item.DepositCode = random.Next(1000, 9999).ToString();
			await _orderPayRep.AddAsync(item);
			response.IsSuccessed = true;
			return response;
		}
	}
}
