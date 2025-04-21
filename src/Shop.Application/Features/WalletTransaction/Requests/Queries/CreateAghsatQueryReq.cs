using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.WalletTransaction;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record CreateAghsatQueryReq(Domain.Models.Order order,int transactionId,int? month) : IRequest<bool>;
    public record CreateAghsatForCreditProductQueryReq(Domain.Models.Order order,CreditProductStockPriceDto credit) : IRequest<bool>;

}
