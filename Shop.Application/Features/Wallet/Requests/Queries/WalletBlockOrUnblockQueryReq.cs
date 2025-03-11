using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Wallet.Requests.Queries
{
    public record WalletBlockOrUnblockQueryReq(bool block, int walletId) : IRequest;
}
