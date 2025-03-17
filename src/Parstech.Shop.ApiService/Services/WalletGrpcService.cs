using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class WalletGrpcService : global::Parstech.Shop.Shared.Protos.Wallet.WalletService.WalletServiceBase
{
    private readonly IMediator _mediator;

    public WalletGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.WalletResponse> GetWalletByUserId(
        global::Parstech.Shop.Shared.Protos.Wallet.WalletRequest request, 
        ServerCallContext context)
    {
        try
        {
            var wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(request.UserId));

            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletResponse
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                Credit = wallet.Credit,
                UsedCredit = wallet.UsedCredit,
                RemainingCredit = wallet.RemainingCredit,
                LastUpdated = wallet.LastUpdated?.ToString() ?? string.Empty,
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto
                {
                    Success = true,
                    Message = "Wallet retrieved successfully"
                }
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse> GetActiveTransaction(
        global::Parstech.Shop.Shared.Protos.Wallet.TransactionRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction =
                await _mediator.Send(new GetActiveAghsatTransactionQueryReq(request.WalletId, request.TypeName));

            if (transaction == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "No active transaction found"));
            }

            return new global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse
            {
                TransactionId = transaction.TransactionId,
                WalletId = transaction.WalletId,
                TypeName = transaction.TypeName,
                Amount = transaction.Amount,
                Description = transaction.Description,
                TrackingCode = transaction.TrackingCode,
                TransactionDate = transaction.TransactionDate.ToString(),
                Months = transaction.Months,
                MonthlyPayment = transaction.MonthlyPayment,
                IsActive = transaction.IsActive,
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto
                {
                    Success = true,
                    Message = "Active transaction retrieved successfully"
                }
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.CalculateResponse> CalculateInstallments(
        global::Parstech.Shop.Shared.Protos.Wallet.CalculateRequest request,
        ServerCallContext context)
    {
        try
        {
            var result =
                await _mediator.Send(new CalculateAghsatQueryReq(request.Price, request.TransactionId, request.Month));

            return new global::Parstech.Shop.Shared.Protos.Wallet.CalculateResponse
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
                MonthlyAmount = result.Object?.MonthlyAmount ?? 0,
                TotalAmount = result.Object?.TotalAmount ?? 0,
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto
                {
                    Success = result.IsSuccessed,
                    Message = result.Message
                }
            };
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.CalculateResponse
            {
                IsSuccessed = false, 
                Message = $"Error calculating installments: {ex.Message}",
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto
                {
                    Success = false,
                    Message = $"Error calculating installments: {ex.Message}"
                }
            };
        }
    }
}