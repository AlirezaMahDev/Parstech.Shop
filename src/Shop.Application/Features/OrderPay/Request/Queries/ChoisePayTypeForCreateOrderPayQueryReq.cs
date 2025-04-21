using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderPay.Request.Queries
{
    public record ChoisePayTypeForCreateOrderPayQueryReq(int payTypeId,Domain.Models.Wallet wallet, Domain.Models.Order order):IRequest<ResponseOrderPayDto>;
    public record ChoisePayTypeForCreateOrderPayForCreditProductQueryReq(int payTypeId,Domain.Models.Wallet wallet, Domain.Models.Order order,CreditProductStockPriceDto credit):IRequest<ResponseOrderPayDto>;

}
