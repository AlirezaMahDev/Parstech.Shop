using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Commands;

public record WalletCreateCommandReq(WalletDto walletDto) : IRequest<WalletDto>;