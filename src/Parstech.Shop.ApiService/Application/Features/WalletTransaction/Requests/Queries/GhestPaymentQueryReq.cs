using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record GhestPaymentQueryReq(int transactionId) : IRequest<ResponseDto>;