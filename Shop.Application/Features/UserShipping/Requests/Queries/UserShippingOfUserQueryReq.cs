using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.UserShipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserShipping.Requests.Queries
{
    public record UserShippingOfUserQueryReq(int userId) : IRequest<List<UserShippingDto>>;
    
}
