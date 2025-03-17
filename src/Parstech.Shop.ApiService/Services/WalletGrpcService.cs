using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Wallet.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class WalletGrpcService : WalletService.WalletServiceBase
{
    private readonly IMediator _mediator;

    public WalletGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<WalletResponse> GetWalletByUserId(WalletRequest request, ServerCallContext context)
    {
        try
        {
            void wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(request.UserId));

            return new WalletResponse
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                Credit = wallet.Credit,
                UsedCredit = wallet.UsedCredit,
                RemainingCredit = wallet.RemainingCredit,
                LastUpdated = wallet.LastUpdated?.ToString() ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<TransactionResponse> GetActiveTransaction(TransactionRequest request,
        ServerCallContext context)
    {
        try
        {
            void transaction =
                await _mediator.Send(new GetActiveAghsatTransactionQueryReq(request.WalletId, request.TypeName));

            if (transaction == null)
            {
                throw new RpcException(new(StatusCode.NotFound, "No active transaction found"));
            }

            return new TransactionResponse
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
                IsActive = transaction.IsActive
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<CalculateResponse> CalculateInstallments(CalculateRequest request,
        ServerCallContext context)
    {
        try
        {
            void result =
                await _mediator.Send(new CalculateAghsatQueryReq(request.Price, request.TransactionId, request.Month));

            return new CalculateResponse
            {
                IsSuccessed = result.IsSuccessed,
                Message = result.Message,
                MonthlyAmount = result.Object?.MonthlyAmount ?? 0,
                TotalAmount = result.Object?.TotalAmount ?? 0
            };
        }
        catch (Exception ex)
        {
            return new CalculateResponse
            {
                IsSuccessed = false, Message = $"Error calculating installments: {ex.Message}"
            };
        }
    }
}