using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class CountOfOpenOrderFromUserQueryHandler : IRequestHandler<CountOfOpenOrderFromUserQueryReq, int>
{
    private readonly IUserRepository _userRep;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;

    public CountOfOpenOrderFromUserQueryHandler(IUserRepository userRep,
        IOrderRepository orderRep,
        IOrderDetailRepository orderDetailRep)
    {
        _userRep = userRep;
        _orderDetailRep = orderDetailRep;
        _orderRep = orderRep;
    }

    public async Task<int> Handle(CountOfOpenOrderFromUserQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        Domain.Models.Order? order = await _orderRep.GetOpenOrderOfUser(user.Id);
        if (order == null)
        {
            return 0;
        }

        return await _orderDetailRep.GetCountOfOrder(order.OrderId);
    }
}