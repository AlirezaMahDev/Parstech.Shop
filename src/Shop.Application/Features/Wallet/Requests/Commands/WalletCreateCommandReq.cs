using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.Wallet;

namespace Shop.Application.Features.Wallet.Requests.Commands
{
    public record WalletCreateCommandReq(WalletDto walletDto) : IRequest<WalletDto>;
}
