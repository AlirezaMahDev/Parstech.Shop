using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

public record GetWalletByUserIdQueryReq(int userId) : IRequest<WalletDto>;