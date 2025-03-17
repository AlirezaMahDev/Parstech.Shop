using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.OrderShipping.Handler.Queries;

public class OrderShippingChangeQueryHandler : IRequestHandler<OrderShippingChangeQueryReq, long>
{
    private readonly IOrderShippingRepository _orderShippingRepository;
    private readonly IShippingTypeRepository _shippingTypeRep;
    private readonly IUserShippingRepository _userShippingRep;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;

    public OrderShippingChangeQueryHandler(IMapper mapper,
        IMediator mediator,
        IOrderShippingRepository orderShippingRepository,
        IOrderRepository orderRepository,
        IUserShippingRepository userShippingRep,
        IShippingTypeRepository shippingTypeRep)
    {
        _orderShippingRepository = orderShippingRepository;
        _mapper = mapper;
        _mediator = mediator;
        _orderRepository = orderRepository;
        _userShippingRep = userShippingRep;
        _shippingTypeRep = shippingTypeRep;
    }

    public async Task<long> Handle(OrderShippingChangeQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.OrderShipping CurrentOrderShipping =
            await _orderShippingRepository.GetOrderShippingByOrderId(request.OrderId);
        int shippingType = 0;
        if (request.Type == "Refresh")
        {
            if (request.OrderSum >= 1000000)
            {
                shippingType = 4;
                ShippingType? shippingTypeItem = await _shippingTypeRep.GetAsync(shippingType);
                return shippingTypeItem.Price;
            }
            else
            {
                if (CurrentOrderShipping.OrderId != 0 && CurrentOrderShipping.UserShippingId != null)
                {
                    Shared.Models.UserShipping? userShipping =
                        await _userShippingRep.GetAsync(int.Parse(CurrentOrderShipping.UserShippingId));
                    if (userShipping.State == "تهران")
                    {
                        shippingType = 1;
                    }
                    else
                    {
                        shippingType = 2;
                    }

                    ShippingType? shippingTypeItem =
                        await _shippingTypeRep.GetAsync(CurrentOrderShipping.ShippingTypeId);
                    return shippingTypeItem.Price;
                }
                else
                {
                    return 0;
                }
            }
        }
        else
        {
            Shared.Models.Order? Order = await _orderRepository.GetAsync(request.OrderId);
            var orderShipping = new Shared.Models.OrderShipping();


            if (request.UserShippingId != 0)
            {
                Shared.Models.UserShipping? userShipping = await _userShippingRep.GetAsync(request.UserShippingId);
                if (userShipping.State == "تهران")
                {
                    shippingType = 1;
                }
                else
                {
                    shippingType = 2;
                }

                if (Order.OrderSum >= 1000000)
                {
                    shippingType = 4;
                }

                orderShipping.FirstName = userShipping.FirstName;
                orderShipping.LastName = userShipping.LastName;
                orderShipping.Phone = userShipping.Phone;
                orderShipping.Mobile = userShipping.Mobile;
                orderShipping.PostCode = userShipping.PostCode;
                orderShipping.UserShippingId = userShipping.Id.ToString();
                orderShipping.FullAddress = $"{userShipping.State},{userShipping.City},{userShipping.Address}";
            }
            else
            {
                shippingType = 3;
                orderShipping.FirstName = "-";
                orderShipping.LastName = "-";
                orderShipping.Phone = "-";
                orderShipping.Mobile = "-";
                orderShipping.PostCode = "-";
                orderShipping.FullAddress = "تحویل به صورت حضوری دفتر مرکزی شرکت پارس تکنولوژی سداد";
            }


            if (CurrentOrderShipping.Id == 0)
            {
                CurrentOrderShipping = new();
                CurrentOrderShipping.FirstName = orderShipping.FirstName;
                CurrentOrderShipping.LastName = orderShipping.LastName;
                CurrentOrderShipping.Phone = orderShipping.Phone;
                CurrentOrderShipping.Mobile = orderShipping.Mobile;
                CurrentOrderShipping.PostCode = orderShipping.PostCode;
                CurrentOrderShipping.FullAddress = orderShipping.FullAddress;
                CurrentOrderShipping.ShippingTypeId = shippingType;
                CurrentOrderShipping.OrderId = Order.OrderId;
                CurrentOrderShipping.UserShippingId = orderShipping.UserShippingId;
                await _orderShippingRepository.AddAsync(CurrentOrderShipping);
            }
            else
            {
                CurrentOrderShipping.FirstName = orderShipping.FirstName;
                CurrentOrderShipping.LastName = orderShipping.LastName;
                CurrentOrderShipping.Phone = orderShipping.Phone;
                CurrentOrderShipping.Mobile = orderShipping.Mobile;
                CurrentOrderShipping.PostCode = orderShipping.PostCode;
                CurrentOrderShipping.FullAddress = orderShipping.FullAddress;
                CurrentOrderShipping.ShippingTypeId = shippingType;
                CurrentOrderShipping.OrderId = Order.OrderId;
                CurrentOrderShipping.UserShippingId = orderShipping.UserShippingId;
                await _orderShippingRepository.UpdateAsync(CurrentOrderShipping);
            }

            ShippingType? shippingTypeItem = await _shippingTypeRep.GetAsync(shippingType);
            //Order.Shipping = shippingTypeItem.Price;
            //await _orderRepository.UpdateAsync(Order);
            return shippingTypeItem.Price;
        }
    }
}