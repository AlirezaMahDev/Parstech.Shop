using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record CreateAghsatQueryReq(Shared.Models.Order order, int transactionId, int? month) : IRequest<bool>;