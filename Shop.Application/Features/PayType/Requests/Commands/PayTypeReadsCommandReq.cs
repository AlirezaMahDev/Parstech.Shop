using MediatR;
using Shop.Application.DTOs.PayType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.PayType.Requests.Commands
{
    public record PayTypeReadsCommandReq(bool justactive):IRequest<List<PayTypeDto>>;

}
