using MediatR;

namespace Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

public record WalletBlockOrUnblockQueryReq(bool block, int walletId) : IRequest;