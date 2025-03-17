using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;

public record GetRemainingOfWalletQueryReq(int userId, string type) : IRequest<long>;