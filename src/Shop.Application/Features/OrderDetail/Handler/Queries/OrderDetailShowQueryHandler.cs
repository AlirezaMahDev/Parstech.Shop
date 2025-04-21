using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderCoupon;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.UserBilling;
using Shop.Application.DTOs.UserShipping;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.PayType.Requests.Commands;
using Shop.Application.Features.UserShipping.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{
    public class OrderDetailShowQueryHandler : IRequestHandler<OrderDetailShowQueryReq, OrderDetailShowDto>
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IOrderCouponRepository _orderCouponRep;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserBillingRepository _userBillingRepository;
        private readonly IUserShippingRepository _userShippingRepository;
        private readonly IOrderShippingRepository _orderShippingRepository;
        private readonly IProductRepository _productRep;
        private readonly IProductGallleryRepository _GalleryRep;
        private readonly ICouponRepository _couponRep;
        private readonly IShippingTypeRepository _shippingTypeRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IProductQueries _productQueries;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IOrderPayRepository _orderPayRep;
        private readonly string _connectionString;


        public OrderDetailShowQueryHandler(IMapper mapper, IMediator mediator,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
             ICouponRepository couponRep,
            IUserBillingRepository userBillingRepository,
            IUserShippingRepository userShippingRepository,
            IOrderShippingRepository orderShippingRepository,
            IOrderCouponRepository orderCouponRep,
            IProductRepository productRep,
            IShippingTypeRepository shippingTypeRep,
            IProductGallleryRepository galleryRep,
            IProductStockPriceRepository productStockRep,
            IProductQueries productQueries,
            IConfiguration configuration,
            IUserStoreRepository userStoreRep, IOrderPayRepository orderPayRep)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userBillingRepository = userBillingRepository;
            _userShippingRepository = userShippingRepository;
            _orderShippingRepository = orderShippingRepository;
            _productRep = productRep;
            _mapper = mapper;
            _mediator = mediator;
            _GalleryRep = galleryRep;
            _orderCouponRep = orderCouponRep;
            _couponRep = couponRep;
            _shippingTypeRep = shippingTypeRep;
            _productStockRep = productStockRep;
            _productQueries = productQueries;
            _userStoreRep = userStoreRep;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _orderPayRep = orderPayRep;
        }
        public async Task<OrderDetailShowDto> Handle(OrderDetailShowQueryReq request, CancellationToken cancellationToken)
        {
            OrderDetailShowDto orderDetailShowDto = new OrderDetailShowDto();
            orderDetailShowDto.OrderDetailDto = new List<OrderDetailDto>();
           
            var order = await _orderRepository.GetAsync(request.orderId);
            if (order == null)
            {
                return orderDetailShowDto;
            }
            var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderId(request.orderId);
            var userBilling = await _userBillingRepository.GetUserBillingByUserId(order.UserId);
            var orderShipping = await _orderShippingRepository.GetOrderShippingByOrderId(request.orderId);
            
            orderDetailShowDto.Order = _mapper.Map<OrderDto>(order);
            orderDetailShowDto.Order.CreateDateShamsi = order.CreateDate.ToShamsi();
            foreach (var item in orderDetails)
            {
                var dto = _mapper.Map<OrderDetailDto>(item);
                var productStock =await _productStockRep.GetAsync(item.ProductStockPriceId);
                var store =await _userStoreRep.GetAsync(productStock.StoreId);
                dto.StoreName = store.StoreName;
                var product = await _productRep.GetAsync(productStock.ProductId);
                var pid = product.Id;
                if (product.ParentId != null)
                {
                    pid = product.ParentId.Value;
                }
                var image = DapperHelper.ExecuteCommand<ProductGalleryDto>(_connectionString, conn => conn.Query<ProductGalleryDto>(_productQueries.GetMainImage, new { @productId = pid }).FirstOrDefault());
                if (image != null)
                {
                    dto.Image = image.ImageName;
                }
              
                dto.ProductName = product.Name;
                dto.ProductCode = product.Code;
               
                
                orderDetailShowDto.OrderDetailDto.Add(dto);
            }
            orderDetailShowDto.OrderShipping = _mapper.Map<OrderShippingDto>(orderShipping);
            if (orderDetailShowDto.OrderShipping.ShippingTypeId!=0)
            {
                var shippingType = await _shippingTypeRep.GetAsync(orderDetailShowDto.OrderShipping.ShippingTypeId);
                orderDetailShowDto.OrderShipping.ShippingType = shippingType.Type;
            }
            else
            {
                var shippingType = await _shippingTypeRep.GetAsync(3);
                orderDetailShowDto.OrderShipping.ShippingType = shippingType.Type;
            }

             var orderPays=await _orderPayRep.GetListByOrderId(order.OrderId);
            orderDetailShowDto.OrderPay = _mapper.Map<List<OrderPayDto>>(orderPays);
            foreach(var orderPay in orderDetailShowDto.OrderPay)
            {
                if (orderPay.PayTypeId == 1)
                {
                    var query = $"select dbo.WalletTransaction.TrackingCode from  dbo.WalletTransaction where Description='{order.OrderCode}' AND Type='Amount' AND TypeId=2";
                    var patTraking = DapperHelper.ExecuteCommand<WalletTransactionDto>(_connectionString, con => con.Query<WalletTransactionDto>(query).FirstOrDefault());
                    if (patTraking != null)
                    {
                        orderPay.PayTracking = patTraking.TrackingCode;
                    }
                }
               
                
            }

            orderDetailShowDto.Costumer = _mapper.Map<UserBillingDto>(userBilling);
			orderDetailShowDto.UserShippingList = await _mediator.Send(new UserShippingOfUserQueryReq(orderDetailShowDto.Order.UserId));

            if(await _orderCouponRep.OrderHaveCoupon(order.OrderId))
            {
                var orderCoupon=await _orderCouponRep.GetByOrderId(order.OrderId);
                var coupon =await _couponRep.GetAsync(orderCoupon.CouponId);
                var orderCuoponDto = _mapper.Map<OrderCouponDto>(orderCoupon);
                switch(coupon.CouponTypeId)
                {
                    case 1: orderCuoponDto.CouponType = "تخفیف ثابت بر روی سبد خرید "; break;
                    case 2: orderCuoponDto.CouponType = "تخفیف درصدی بر روی سبد خرید"; break;
                    case 3: orderCuoponDto.CouponType = "تخفیف ثابت بر روی اقلام سفارش"; break;
                    case 4: orderCuoponDto.CouponType = "تخفیف درصدی بر روی اقلام سفارش"; break;
                    default:break;
                }
                orderDetailShowDto.OrderCoupon = orderCuoponDto;
            }
            else
            {
                orderDetailShowDto.OrderCoupon = new OrderCouponDto();
                orderDetailShowDto.OrderCoupon.DiscountPrice = 0;
                orderDetailShowDto.OrderCoupon.CouponType = "-";
            }

            orderDetailShowDto.PayTypes= await _mediator.Send(new PayTypeReadsCommandReq(true));

            return orderDetailShowDto;

        }
    }
}
