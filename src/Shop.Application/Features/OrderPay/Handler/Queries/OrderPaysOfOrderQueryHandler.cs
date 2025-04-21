using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.OrderPay.Request.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Handler.Queries
{
	public class OrderPaysOfOrderQueryHandler : IRequestHandler<OrderPaysOfOrderQueryReq, ResponseDto>
	{
		private readonly IOrderPayRepository _orderPayRep;
		private readonly IPayTypeRepository _payTypeRep;
		private readonly IMapper _mapper;
        public OrderPaysOfOrderQueryHandler(IOrderPayRepository orderPayRep, IMapper mapper, IPayTypeRepository payTypeRep)
        {
            _orderPayRep=orderPayRep;
			_mapper=mapper;
			_payTypeRep=payTypeRep;
        }
        public async Task<ResponseDto> Handle(OrderPaysOfOrderQueryReq request, CancellationToken cancellationToken)
		{
			ResponseDto response=new ResponseDto();
			var list =await _orderPayRep.GetListByOrderId(request.orderId);
			List<OrderPayDto> listDto = new List<OrderPayDto>();
			foreach (var item in list)
			{
				var dto=_mapper.Map<OrderPayDto>(item);
				var payType =await _payTypeRep.GetAsync(item.PayTypeId);
				dto.TypeName = payType.TypeName;
				listDto.Add(dto);
			}
			response.IsSuccessed = true;
			response.Object = listDto;
			return response;
		}
	}
}
