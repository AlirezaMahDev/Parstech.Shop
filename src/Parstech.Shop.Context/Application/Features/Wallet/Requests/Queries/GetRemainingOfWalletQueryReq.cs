using MediatR;

namespace Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

public record GetRemainingOfWalletQueryReq(int userId, string type):IRequest<long>;