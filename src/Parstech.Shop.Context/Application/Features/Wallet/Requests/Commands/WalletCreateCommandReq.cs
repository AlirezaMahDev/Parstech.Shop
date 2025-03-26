using MediatR;

using Parstech.Shop.Context.Application.DTOs.Wallet;

namespace Parstech.Shop.Context.Application.Features.Wallet.Requests.Commands;

public record WalletCreateCommandReq(WalletDto walletDto) : IRequest<WalletDto>;