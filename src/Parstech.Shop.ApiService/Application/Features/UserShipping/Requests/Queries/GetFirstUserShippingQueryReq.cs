using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserShipping.Requests.Queries
{
    public record GetFirstUserShippingQueryReq(int userId):IRequest<int>;

}
