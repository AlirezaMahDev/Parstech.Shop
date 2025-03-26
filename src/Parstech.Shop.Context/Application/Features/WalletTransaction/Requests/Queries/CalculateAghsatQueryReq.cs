using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record CalculateAghsatQueryReq(long price,int transactionId,int month):IRequest<ResponseDto>;