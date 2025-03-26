using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record GhestPaymentQueryReq(int transactionId):IRequest<ResponseDto>;