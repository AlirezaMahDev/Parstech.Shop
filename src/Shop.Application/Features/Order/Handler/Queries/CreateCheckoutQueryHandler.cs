using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Domain.Models;
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

    public class CreateCheckoutForCreditProductQueryHandler : IRequestHandler<CreateCheckoutForCreditProductQueryReq, OrderDto>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRep;
        private readonly IWalletTransactionRepository _waletTransactionRep;
        private readonly IMapper _mapper;

        public CreateCheckoutForCreditProductQueryHandler(IMediator mediator,
            IUserRepository userRep,
            IMapper mapper,
            IWalletTransactionRepository waletTransactionRep)
        {
            _userRep = userRep;
            _mediator = mediator;
            _mapper = mapper;
            _waletTransactionRep = waletTransactionRep;
        }
        public async Task<OrderDto> Handle(CreateCheckoutForCreditProductQueryReq request, CancellationToken cancellationToken)
        {
            OrderDto result = new OrderDto();
            if (request.userName == "-")
            {
               
                return result;
            }
            
           
            var user = await _userRep.GetUserByUserName(request.userName);
            if (await _waletTransactionRep.ExistCreditForUser(user.Id))
            {

                return result;
            }
            var order = await _mediator.Send(new OrderCreateByUserIdQueryReq(user.Id));
            
            var res = await _mediator.Send(new OrderDetailCreateByProductAndOrderIdForCreditProductQueryReq(order.OrderId, request.credit.Id, request.userName));
            result = _mapper.Map<OrderDto>(order) ;
            return result;
        }
    }
}
