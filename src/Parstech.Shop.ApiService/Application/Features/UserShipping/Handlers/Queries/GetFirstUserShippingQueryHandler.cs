using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.UserShipping.Handlers.Queries;

public class GetFirstUserShippingQueryHandler : IRequestHandler<GetFirstUserShippingQueryReq, int>
{
    private readonly IUserShippingRepository _userShippingRep;

    public GetFirstUserShippingQueryHandler(IUserShippingRepository userShippingRep)
    {
        _userShippingRep = userShippingRep;
    }

    public async Task<int> Handle(GetFirstUserShippingQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.UserShipping? item = await _userShippingRep.GetFirstShippingOfUser(request.userId);
        if (item == null)
        {
            return 0;
        }

        return item.Id;
    }
}