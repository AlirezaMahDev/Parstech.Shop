using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Enum;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

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
        ResponseDto response = new();
        Shared.Models.Order? order = await _orderRep.GetAsync(request.orderId);

        List<Shared.Models.OrderDetail> orderDetails = await _orderDetailRep.GetOrderDetailsByOrderId(order.OrderId);

        ProductRepresentationDto productRep = new() { UserId = order.UserId, TypeId = 2 };
        int coin = 0;

        //کم کردن موجودی
        foreach (Shared.Models.OrderDetail orderDetail in orderDetails)
        {
            Shared.Models.ProductStockPrice? productStock =
                await _productStockRep.GetAsync(orderDetail.ProductStockPriceId);
            Shared.Models.Product? product = await _productRep.GetAsync(productStock.ProductId);

            if (product.TypeId == 4)
            {
                await _mediator.Send(new CreateProductRepresentationForChildsOfBundleQueryReq(order.UserId,
                    product.Id,
                    productStock.Id,
                    orderDetail.Count));
            }
            else
            {
                coin += product.Score;
                productRep.UniqeCode = CodeGenerator.GenerateUniqCode();
                productRep.ProductStockPriceId = productStock.Id;
                productRep.RepresntationId = productStock.RepId;
                productRep.Quantity = orderDetail.Count;
                await _mediator.Send(new ProductRepresesntationCreateCommandReq(productRep));
            }
        }

        if (request.transactionId != 0)
        {
            Shared.Models.WalletTransaction? transaction = await _walletTransactionRep.GetAsync(request.transactionId);
            //واریز و برداشت پس از پرداخت اینترنتی
            if (transaction.TypeId == 3)
            {
                transaction.TypeId = 2;
                transaction.TrackingCode = $"کد پیگیری پرداخت : {request.transactionId}";
                await _walletTransactionRep.UpdateAsync(transaction);
                WalletTransactionDto? transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
                await _walletRep.WalletCalculateTransaction(transactionDto);

                Shared.Models.WalletTransaction bardasht = new()
                {
                    WalletId = transaction.WalletId,
                    TypeId = 1,
                    Price = transaction.Price,
                    Type = transaction.Type,
                    TrackingCode = CodeGenerator.GenerateUniqCode(),
                    Description = transaction.Description,
                    CreateDate = DateTime.Now
                };
                await _walletTransactionRep.AddAsync(bardasht);
                WalletTransactionDto? bardashtDto = _mapper.Map<WalletTransactionDto>(bardasht);
                await _walletRep.WalletCalculateTransaction(bardashtDto);
            }
            //برداشت از حساب
            else
            {
                WalletTransactionDto? transactionDto = _mapper.Map<WalletTransactionDto>(transaction);
                await _walletRep.WalletCalculateTransaction(transactionDto);
            }

            response.Object2 = transaction.Description;
        }


        Shared.Models.Wallet wallet = await _walletRep.GetWalletByUserId(order.UserId);
        //امتیازات
        Shared.Models.WalletTransaction coinTransaction = new()
        {
            WalletId = wallet.WalletId,
            TypeId = 2,
            Price = coin,
            Type = "Coin",
            TrackingCode = CodeGenerator.GenerateUniqCode(),
            Description = $"افزایش امتیاز سفارش {order.OrderCode}",
            CreateDate = DateTime.Now
        };
        await _walletTransactionRep.AddAsync(coinTransaction);
        WalletTransactionDto? coinDto = _mapper.Map<WalletTransactionDto>(coinTransaction);
        await _walletRep.WalletCalculateTransaction(coinDto);


        // کم کردن تخفیف
        if (await _orderCouponRep.ExistInOrder(order.OrderId))
        {
            OrderCoupon? orderCoupon = await _orderCouponRep.GetByOrderId(order.OrderId);
            Shared.Models.Coupon? coupon = await _couponRep.GetAsync(orderCoupon.CouponId);
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


        response.IsSuccessed = true;
        response.Message = "سفارش شما با موفقیت پرداخت شد";
        //Response.Object = $"{transaction.TrackingCode}";

        return response;
    }
}