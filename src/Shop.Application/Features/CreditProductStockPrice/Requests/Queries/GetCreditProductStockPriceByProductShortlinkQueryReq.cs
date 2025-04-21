using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Requests.Queries
{
    
    public record GetCreditProductStockPriceByProductShortlinkQueryReq(string shortLink) : IRequest<List<CreditProductStockPriceDto>?>;

}
