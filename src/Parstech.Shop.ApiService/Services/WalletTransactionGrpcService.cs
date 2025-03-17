using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class WalletTransactionGrpcService : global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionService.WalletTransactionServiceBase
{
    private readonly IMediator _mediator;
    private readonly IWalletTransactionRepository _walletTransactionRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IUserRepository _userRepository;

    public WalletTransactionGrpcService(
        IMediator mediator,
        IWalletTransactionRepository walletTransactionRepository,
        IWalletRepository walletRepository,
        IUserRepository userRepository)
    {
        _mediator = mediator;
        _walletTransactionRepository = walletTransactionRepository;
        _walletRepository = walletRepository;
        _userRepository = userRepository;
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse> GetWalletTransaction(
        global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse { Status = false, Message = "Transaction not found" };
            }

            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse
            {
                Status = true,
                Message = "Transaction found",
                Transaction = new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionItem
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    TransactionTypeId = transaction.TypeId,
                    TransactionTypeName = transaction.TypeNavigation?.TypeTitle ?? string.Empty,
                    Amount = transaction.Price,
                    Description = transaction.Description ?? string.Empty,
                    TrackingCode = transaction.TrackingCode ?? string.Empty,
                    TransactionDate = transaction.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsSuccess = transaction.Active ?? false,
                    Token = transaction.TrackingCode ?? string.Empty,
                    OrderId = 0
                }
            };
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse
            {
                Status = false, Message = $"Error retrieving transaction: {ex.Message}"
            };
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse> GetWalletTransactionByToken(
        global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionTokenRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByTokenAsync(request.Token);
            if (transaction == null)
            {
                return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse { Status = false, Message = "Transaction not found" };
            }

            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse
            {
                Status = true,
                Message = "Transaction found",
                Transaction = new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionItem
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    TransactionTypeId = transaction.TypeId,
                    TransactionTypeName = transaction.TypeNavigation?.TypeTitle ?? string.Empty,
                    Amount = transaction.Price,
                    Description = transaction.Description ?? string.Empty,
                    TrackingCode = transaction.TrackingCode ?? string.Empty,
                    TransactionDate = transaction.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsSuccess = transaction.Active ?? false,
                    Token = transaction.TrackingCode ?? string.Empty,
                    OrderId = 0
                }
            };
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionResponse
            {
                Status = false, Message = $"Error retrieving transaction: {ex.Message}"
            };
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionUpdateResponse> UpdateWalletTransaction(
        global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionUpdateRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionUpdateResponse { Status = false, Message = "Transaction not found" };
            }

            transaction.Active = request.IsSuccess;
            if (!string.IsNullOrEmpty(request.TrackingCode))
            {
                transaction.TrackingCode = request.TrackingCode;
            }

            await _walletTransactionRepository.UpdateAsync(transaction);

            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionUpdateResponse { Status = true, Message = "Transaction updated successfully" };
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionUpdateResponse
            {
                Status = false, Message = $"Error updating transaction: {ex.Message}"
            };
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.TransactionHistoryResponse> GetTransactionHistory(
        global::Parstech.Shop.Shared.Protos.Wallet.TransactionHistoryRequest request,
        ServerCallContext context)
    {
        try
        {
            var query = new GetWalletTransactionHistoryQueryReq(
                request.WalletId,
                request.UserId,
                request.Page,
                request.PageSize,
                request.FromDate,
                request.ToDate,
                request.TransactionTypeId);

            var result = await _mediator.Send(query);

            var response = new global::Parstech.Shop.Shared.Protos.Wallet.TransactionHistoryResponse
            {
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto { IsSuccess = true, Message = "Transaction history retrieved successfully" }
            };

            foreach (var transaction in result.Transactions)
            {
                response.Transactions.Add(new global::Parstech.Shop.Shared.Protos.Wallet.WalletTransactionItem
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionTypeName = transaction.TransactionTypeName,
                    Amount = transaction.Amount,
                    Description = transaction.Description ?? string.Empty,
                    TrackingCode = transaction.TrackingCode ?? string.Empty,
                    TransactionDate = transaction.TransactionDate,
                    IsSuccess = transaction.IsSuccess,
                    Token = transaction.Token ?? string.Empty,
                    OrderId = transaction.OrderId ?? 0
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.TransactionHistoryResponse
            {
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto { IsSuccess = false, Message = $"Error retrieving transaction history: {ex.Message}" }
            };
        }
    }

    public override async Task<global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse> GetActiveTransaction(
        global::Parstech.Shop.Shared.Protos.Wallet.TransactionRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetActiveAghsat(request.WalletId, request.TypeName);

            if (transaction == null)
            {
                return new global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse
                {
                    Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto { IsSuccess = false, Message = "No active transaction found" }
                };
            }

            return new global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse
            {
                TransactionId = transaction.Id,
                WalletId = transaction.WalletId,
                TypeName = transaction.Type,
                Amount = transaction.Price,
                Description = transaction.Description ?? string.Empty,
                TrackingCode = transaction.TrackingCode ?? string.Empty,
                TransactionDate = transaction.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Months = transaction.Month ?? 0,
                MonthlyPayment = transaction.Price,
                IsActive = transaction.Active ?? false,
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto { IsSuccess = true, Message = "Active transaction retrieved successfully" }
            };
        }
        catch (Exception ex)
        {
            return new global::Parstech.Shop.Shared.Protos.Wallet.TransactionResponse
            {
                Status = new global::Parstech.Shop.Shared.Protos.Common.ResponseDto { IsSuccess = false, Message = $"Error retrieving active transaction: {ex.Message}" }
            };
        }
    }
} 