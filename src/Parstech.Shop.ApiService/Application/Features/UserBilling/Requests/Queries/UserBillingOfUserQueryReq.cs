using MediatR;
using Shop.Application.DTOs.UserBilling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserBilling.Requests.Queries
{
    public record UserBillingOfUserQueryReq(int userId):IRequest<UserBillingDto>;
}
