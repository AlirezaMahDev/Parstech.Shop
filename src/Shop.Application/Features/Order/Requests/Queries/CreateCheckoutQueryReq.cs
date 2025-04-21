using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
    public record CreateCheckoutQueryReq(string userName, int productId) : IRequest<ResponseDto>;
    public record CreateCheckoutForCreditProductQueryReq(string userName, CreditProductStockPriceDto credit) : IRequest<OrderDto>;
}
