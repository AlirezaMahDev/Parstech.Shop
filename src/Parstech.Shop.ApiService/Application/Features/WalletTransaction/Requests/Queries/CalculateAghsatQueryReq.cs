using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record CalculateAghsatQueryReq(long price, int transactionId, int month) : IRequest<ResponseDto>;