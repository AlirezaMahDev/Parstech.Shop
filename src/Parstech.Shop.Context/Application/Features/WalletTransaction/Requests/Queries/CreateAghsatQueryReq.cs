using MediatR;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record CreateAghsatQueryReq(Domain.Models.Order order,int transactionId,int? month) : IRequest<bool>;