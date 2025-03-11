using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.UserShipping.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserShipping.Handlers.Queries
{
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
}
