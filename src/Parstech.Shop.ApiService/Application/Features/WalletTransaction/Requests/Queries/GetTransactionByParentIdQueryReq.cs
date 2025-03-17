using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record GetTransactionByParentIdQueryReq(int id) : IRequest<List<WalletTransactionDto>>;