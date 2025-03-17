using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class CallBackCompleteOrderQueryHandler : IRequestHandler<CallBackCompleteOrderQueryReq, ResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IOrderCouponRepository _orderCouponRep;
    private readonly ICouponRepository _couponRep;
    private readonly IWalletRepository _walletRep;
    private readonly IWalletTransactionRepository _walletTransactionRep;
    private readonly IProductRepository _productRep;
    private readonly IProductRepresesntationRepository _productReperesntationRep;
    private readonly IProductStockPriceRepository _productStockRep;

    public CallBackCompleteOrderQueryHandler(IMediator mediator,
        IMapper mapper,
        IOrderRepository orderRep,
        IOrderPayRepository orderPayRep,
        IOrderDetailRepository orderDetailRep,
        IOrderCouponRepository orderCouponRep,
        ICouponRepository couponRep,
        IWalletRepository walletRep,
        IWalletTransactionRepository walletTransactionRep,
        IProductRepository productRep,
        IProductStockPriceRepository productStockRep,
        IProductRepresesntationRepository productReperesntationRep)
    {
        _mediator = mediator;
        _mapper = mapper;
        _orderRep = orderRep;
        _orderPayRep = orderPayRep;
        _orderDetailRep = orderDetailRep;
        _orderCouponRep = orderCouponRep;
        _couponRep = couponRep;
        _walletRep = walletRep;
        _walletTransactionRep = walletTransactionRep;
        _productRep = productRep;
        _productReperesntationRep = productReperesntationRep;
        _productStockRep = productStockRep;
    }


    public async Task<ResponseDto> Handle(CallBackCompleteOrderQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        Domain.Models.Order? order = await _orderRep.GetAsync(request.orderId);

        List<Domain.Models.OrderDetail> orderDetails = await _orderDetailRep.GetOrderDetailsByOrderId(order.OrderId);

        ProductRepresentationDto ProductRep = new() { UserId = order.UserId, TypeId = 2 };
        int Coin = 0;

        //کم کردن موجودی
        foreach (Domain.Models.OrderDetail orderDetail in orderDetails)
        {
            Domain.Models.ProductStockPrice? productStock =
                await _productStockRep.GetAsync(orderDetail.ProductStockPriceId);
            Domain.Models.Product? product = await _productRep.GetAsync(productStock.ProductId);

            if (product.TypeId == 4)
            {
                await _mediator.Send(new CreateProductRepresentationForChildsOfBundleQueryReq(order.UserId,
                    product.Id,
                    productStock.Id,
                    orderDetail.Count));
                ;
            }
            else
            {
                Coin += product.Score;
                ProductRep.UniqeCode = CodeGenerator.GenerateUniqCode();
                ProductRep.ProductStockPriceId = productStock.Id;
                ProductRep.RepresntationId = productStock.RepId;
                ProductRep.Quantity = orderDetail.Count;
                await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRep));
            }
        }

        if (request.transactionId != 0)
        {
            Domain.Models.WalletTransaction? transaction = await _walletTransactionRep.GetAsync(request.transactionId);
            //واریز و برداشت پس از پرداخت اینترنتی
            if (transaction.TypeId == 3)
            {
                transaction.TypeId = 2;
                transaction.TrackingCode = $"کد پیگیری پرداخت : {request.transactionId}";
                await _walletTransactionRep.UpdateAsync(transaction);
                WalletTransactionDto? transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
                await _walletRep.WalletCalculateTransaction(transactionDto);

                Shop.Domain.Models.WalletTransaction Bardasht = new Shop.Domain.Models.WalletTransaction()
                {
                    WalletId = transaction.WalletId,
                    TypeId = 1,
                    Price = transaction.Price,
                    Type = transaction.Type,
                    TrackingCode = CodeGenerator.GenerateUniqCode(),
                    Description = transaction.Description,
                    CreateDate = DateTime.Now
                };
                await _walletTransactionRep.AddAsync(Bardasht);
                WalletTransactionDto? BardashtDto = _mapper.Map<WalletTransactionDto>(Bardasht);
                await _walletRep.WalletCalculateTransaction(BardashtDto);
            }
            //برداشت از حساب
            else
            {
                WalletTransactionDto? transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
                await _walletRep.WalletCalculateTransaction(transactionDto);
            }

            Response.Object2 = transaction.Description;
        }


        Domain.Models.Wallet wallet = await _walletRep.GetWalletByUserId(order.UserId);
        //امتیازات
        Shop.Domain.Models.WalletTransaction CoinTransaction = new Shop.Domain.Models.WalletTransaction()
        {
            WalletId = wallet.WalletId,
            TypeId = 2,
            Price = Coin,
            Type = "Coin",
            TrackingCode = CodeGenerator.GenerateUniqCode(),
            Description = $"افزایش امتیاز سفارش {order.OrderCode}",
            CreateDate = DateTime.Now
        };
        await _walletTransactionRep.AddAsync(CoinTransaction);
        WalletTransactionDto? CoinDto = _mapper.Map<WalletTransactionDto>(CoinTransaction);
        await _walletRep.WalletCalculateTransaction(CoinDto);


        // کم کردن تخفیف
        if (await _orderCouponRep.ExistInOrder(order.OrderId))
        {
            OrderCoupon? orderCoupon = await _orderCouponRep.GetByOrderId(order.OrderId);
            Domain.Models.Coupon? coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
            if (coupon.CouponTypeId == 2)
            {
                coupon.Amount -= orderCoupon.DiscountPrice;
            }

            coupon.LimitUse--;
            await _couponRep.UpdateAsync(coupon);
        }

        order.IsFinaly = true;
        order.ConfirmPayment = true;
        await _orderRep.UpdateAsync(order);

        ////ثیت وضعیت
        await _mediator.Send(new CreateOrderStatusByStatusIdQueryReq(OrderStatusType.OrderDoing.ToString(),
            order.OrderId,
            order.UserId));


        Response.IsSuccessed = true;
        Response.Message = "سفارش شما با موفقیت پرداخت شد";
        //Response.Object = $"{transaction.TrackingCode}";

        return Response;
    }
}