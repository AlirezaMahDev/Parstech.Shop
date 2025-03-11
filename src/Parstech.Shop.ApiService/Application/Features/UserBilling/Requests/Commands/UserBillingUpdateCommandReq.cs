using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.UserBilling;

namespace Shop.Application.Features.UserBilling.Requests.Commands
{
    public record UserBillingUpdateCommandReq(UserBillingDto userBillingDto) : IRequest<UserBillingDto>;
}
