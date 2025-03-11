using MediatR;
using Shop.Application.DTOs.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record GetTransactionByParentIdQueryReq(int id):IRequest<List<WalletTransactionDto>>;

}
