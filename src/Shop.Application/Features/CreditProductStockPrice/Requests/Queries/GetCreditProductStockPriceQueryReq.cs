using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Requests.Queries
{
    public record GetCreditProductStockPriceQueryReq(int? id,int productStocPriceId):IRequest<CreditProductStockPriceDto?>;

}
