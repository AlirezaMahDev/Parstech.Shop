using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Response;
using Shop.Application.Enum;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{
    

    public class OrderDetailCreateByProductAndOrderIdQueryHandler : IRequestHandler<OrderDetailCreateByProductAndOrderIdQueryReq, ResponseDto>
    {
        private IOrderRepository _orderRep;
        private IUserRepository _userRep;
        private IOrderDetailRepository _orderDetailRep;
        private IProductRepository  _productRep;
        private IProductStockPriceRepository  _productStockRep;
        private IMapper _mapper;
        private IMediator _mediator;

        public OrderDetailCreateByProductAndOrderIdQueryHandler(IOrderRepository orderRep,
            IOrderDetailRepository orderDetailRep,
            IProductRepository productRep,
            IMapper mapper,
            IMediator mediator,
            IProductStockPriceRepository productStockRep,
            IUserRepository userRep)
        {
            _orderRep = orderRep;
            _orderDetailRep = orderDetailRep;
            _mapper = mapper;
            _mediator = mediator;
            _productRep = productRep;
            _productStockRep = productStockRep;
            _userRep = userRep;
        }

        public async Task<ResponseDto> Handle(OrderDetailCreateByProductAndOrderIdQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto result = new ResponseDto();
            Shop.Domain.Models.OrderDetail item = new Shop.Domain.Models.OrderDetail();
            var productStock =await _productStockRep.GetAsync(request.productId);
            item.ProductStockPriceId = productStock.Id;
            long Price =0;





            #region Check UserCategury
            var existUserCategury = false;
            if (request.userName != null)
            {
                existUserCategury = await _userRep.ExistUserCategury(request.userName);
            }
            if (productStock.CateguryOfUserId != null)
            {
                if (!existUserCategury)
                {
                    if (productStock.CateguryOfUserType == CateguryOfUserType.ShowDiscoutProductForUserCategury.ToString())
                    {
                        Price = productStock.SalePrice;
                    }
                    else if (productStock.CateguryOfUserType == CateguryOfUserType.ShowProductJustForUserCategury.ToString())
                    {
                        Price = 0;
                    }
                }
                else
                {
                    if (productStock.DiscountPrice > 0)
                    {
                        Price = productStock.DiscountPrice;
                    }
                    else
                    {
                        Price = productStock.SalePrice;
                    }
                }
            }
            else
            {
                if (productStock.DiscountPrice > 0)
                {
                    Price = productStock.DiscountPrice;
                }
                else
                {
                    Price = productStock.SalePrice;
                }
            }
            #endregion





            
            item.Count = 1;
            item.Price = Price;
            item.OrderId = request.orderId;
            item.DetailSum = 0;
            item.Tax = 0;
            item.Discount = 0;
            //item.ProductId = request.productId;
            item.Total = 0;
            
            //var orderDetail = _mapper.Map<Domain.Models.OrderDetail>(item);

            var orderResult = await _orderDetailRep.AddAsync(item);
            var refreshDetail = _mapper.Map<OrderDetailDto>(orderResult);
             await _mediator.Send(new RefreshOrderDetailQueryReq(refreshDetail));
            result.IsSuccessed=true;
            result.Message = "محصول با موفقیت به سبد خرید شما اضافه شد";
            return result;
        }
    }
}
