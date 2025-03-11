using MediatR;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Request.Command
{
	public record OrderPayCreateCommandReq(OrderPayDto orderPayDto):IRequest<ResponseDto>;

}
