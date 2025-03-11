using Shop.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shop.Application.Features.User.Requests.Queries
{
    public record UserPagingQueryReq(UserParameterDto UserParameterDto) : IRequest<UserPageingDto>;
}
