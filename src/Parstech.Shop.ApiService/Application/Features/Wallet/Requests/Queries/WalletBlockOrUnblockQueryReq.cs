using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

public record WalletBlockOrUnblockQueryReq(bool block, int walletId) : IRequest;