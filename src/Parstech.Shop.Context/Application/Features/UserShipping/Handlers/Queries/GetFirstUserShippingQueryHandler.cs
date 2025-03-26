using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.UserShipping.Handlers.Queries;

public class GetFirstUserShippingQueryHandler : IRequestHandler<GetFirstUserShippingQueryReq, int>
{
    private readonly IUserShippingRepository _userShippingRep;
    public GetFirstUserShippingQueryHandler(IUserShippingRepository userShippingRep)
    {
        _userShippingRep= userShippingRep;
    }
    public async Task<int> Handle(GetFirstUserShippingQueryReq request, CancellationToken cancellationToken)
    {
        var item =await _userShippingRep.GetFirstShippingOfUser(request.userId);
        if (item == null)
        {
            return 0;
        }
        return item.Id;
    }
}