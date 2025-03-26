using MediatR;

using Parstech.Shop.Context.Application.DTOs.Wallet;

namespace Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

public record GetWalletByUserIdQueryReq(int userId):IRequest<WalletDto>;