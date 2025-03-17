using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Commands;

public record WalletCreateCommandReq(WalletDto walletDto) : IRequest<WalletDto>;