using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record EditStartOrActiveTransactionQueryReq(int transactionId,string startOrActive,bool? start,bool? active):IRequest;

}
