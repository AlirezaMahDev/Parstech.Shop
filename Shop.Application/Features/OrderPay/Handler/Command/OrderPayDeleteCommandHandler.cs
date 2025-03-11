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
	public class OrderPayDeleteCommandHandler : IRequestHandler<OrderPayDeleteCommandReq, ResponseDto>
	{
		private readonly IOrderPayRepository _orderPayRep;
		private readonly IOrderRepository _orderRep;
        public OrderPayDeleteCommandHandler(IOrderPayRepository orderPayRep, IOrderRepository orderRep)
        {
            _orderPayRep = orderPayRep;
			_orderRep = orderRep;
        }
        public async Task<ResponseDto> Handle(OrderPayDeleteCommandReq request, CancellationToken cancellationToken)
		{
			ResponseDto response=new ResponseDto();
			var item =await _orderPayRep.GetAsync(request.id);
			var order =await _orderRep.GetAsync(item.OrderId);
			if (!order.IsFinaly)
			{
				await _orderPayRep.DeleteAsync(item);
				response.IsSuccessed = true;
				response.Message = "روش پرداخت با موفقیت حذف گردید";
			}
			else
			{
				response.IsSuccessed = false;
				response.Message = "سفارش تکمیل شده امکان حذف وش پرداخت ندارد";
			}
			
			return response;
		}
	}
}
