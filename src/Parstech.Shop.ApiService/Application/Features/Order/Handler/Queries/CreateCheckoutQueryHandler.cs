using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
    public class CreateCheckoutQueryHandler : IRequestHandler<CreateCheckoutQueryReq, ResponseDto>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRep;
        private readonly IOrderRepository _orderRep;
        private readonly IOrderDetailRepository _orderDetailRep;
        public CreateCheckoutQueryHandler(IMediator mediator,
            IUserRepository userRep,
            IOrderRepository orderRep,
            IOrderDetailRepository orderDetailRep)
        {
            _userRep = userRep; 
            _mediator = mediator;
            _orderRep = orderRep;
            _orderDetailRep = orderDetailRep;
        }
        public async Task<ResponseDto> Handle(CreateCheckoutQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto result =new ResponseDto();
            if(request.userName=="-")
            {
                result.IsSuccessed = false;
                result.Message = "ابتدا وارد حساب خود شوید";
                return result;
            }
            var order =await _mediator.Send(new GetOpenOrderOfUserQueryReq(request.userName));
            if (order.OrderId==0)
            {
                var user=await _userRep.GetUserByUserName(request.userName);
                order=await _mediator.Send(new OrderCreateByUserIdQueryReq(user.Id));
            }

            if (await _orderDetailRep.ProductIdExistInOrderDetails(order.OrderId, request.productId))
            {
                result.IsSuccessed = false;
                result.Message = "این محصول قبلا به سبد خرید اضافه شده است";
            }
            else
            {
                result = await _mediator.Send(new OrderDetailCreateByProductAndOrderIdQueryReq(order.OrderId,request.productId,request.userName));
            }
            return result;
        }
    }
}
