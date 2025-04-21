using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Requests.Commands
{
    public record CreateOrUpdateCreditCommandReq(CreditProductStockPriceDto credit):IRequest<ResponseDto>;

}
