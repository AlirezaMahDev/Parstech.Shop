using MediatR;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record WalletTransactionDetailShowQueryReq(int transactionId): IRequest<WalletTransactionDto>;
}
