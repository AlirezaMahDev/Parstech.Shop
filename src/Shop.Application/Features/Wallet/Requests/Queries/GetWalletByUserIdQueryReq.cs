using MediatR;
using Shop.Application.DTOs.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Wallet.Requests.Queries
{
    public record GetWalletByUserIdQueryReq(int userId):IRequest<WalletDto>;
}
