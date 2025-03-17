using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;

namespace Parstech.Shop.ApiService.Services;

public class WalletTransactionGrpcService : WalletService.WalletServiceBase
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

    public override async Task<WalletTransactionResponse> GetWalletTransaction(
        WalletTransactionRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return new WalletTransactionResponse { Status = false, Message = "Transaction not found" };
            }

            return new WalletTransactionResponse
            {
                Status = true,
                Message = "Transaction found",
                Transaction = new WalletTransactionItem
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionTypeName = transaction.TransactionType?.Name ?? string.Empty,
                    Amount = transaction.Amount,
                    Description = transaction.Description ?? string.Empty,
                    TrackingCode = transaction.TrackingCode ?? string.Empty,
                    TransactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsSuccess = transaction.IsSuccess,
                    Token = transaction.Token ?? string.Empty,
                    OrderId = transaction.OrderId ?? 0
                }
            };
        }
        catch (Exception ex)
        {
            return new WalletTransactionResponse
            {
                Status = false, Message = $"Error retrieving transaction: {ex.Message}"
            };
        }
    }

    public override async Task<WalletTransactionResponse> GetWalletTransactionByToken(
        WalletTransactionTokenRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByTokenAsync(request.Token);
            if (transaction == null)
            {
                return new WalletTransactionResponse { Status = false, Message = "Transaction not found" };
            }

            return new WalletTransactionResponse
            {
                Status = true,
                Message = "Transaction found",
                Transaction = new WalletTransactionItem
                {
                    Id = transaction.Id,
                    WalletId = transaction.WalletId,
                    TransactionTypeId = transaction.TransactionTypeId,
                    TransactionTypeName = transaction.TransactionType?.Name ?? string.Empty,
                    Amount = transaction.Amount,
                    Description = transaction.Description ?? string.Empty,
                    TrackingCode = transaction.TrackingCode ?? string.Empty,
                    TransactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsSuccess = transaction.IsSuccess,
                    Token = transaction.Token ?? string.Empty,
                    OrderId = transaction.OrderId ?? 0
                }
            };
        }
        catch (Exception ex)
        {
            return new WalletTransactionResponse
            {
                Status = false, Message = $"Error retrieving transaction: {ex.Message}"
            };
        }
    }

    public override async Task<WalletTransactionUpdateResponse> UpdateWalletTransaction(
        WalletTransactionUpdateRequest request,
        ServerCallContext context)
    {
        try
        {
            var transaction = await _walletTransactionRepository.GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                return new WalletTransactionUpdateResponse { Status = false, Message = "Transaction not found" };
            }

            transaction.IsSuccess = request.IsSuccess;
            if (!string.IsNullOrEmpty(request.TrackingCode))
            {
                transaction.TrackingCode = request.TrackingCode;
            }

            await _walletTransactionRepository.UpdateAsync(transaction);

            return new WalletTransactionUpdateResponse { Status = true, Message = "Transaction updated successfully" };
        }
        catch (Exception ex)
        {
            return new WalletTransactionUpdateResponse
            {
                Status = false, Message = $"Error updating transaction: {ex.Message}"
            };
        }
    }

    public override async Task<UserWalletResponse> GetUserWallet(
        UserWalletRequest request,
        ServerCallContext context)
    {
        try
        {
            var user = await _userRepository.GetByUserNameAsync(request.UserName);
            if (user == null)
            {
                return new UserWalletResponse { Status = false, Message = "User not found" };
            }

            var wallet = await _walletRepository.GetByUserIdAsync(user.Id);
            if (wallet == null)
            {
                return new UserWalletResponse { Status = false, Message = "Wallet not found" };
            }

            return new UserWalletResponse
            {
                Status = true,
                Message = "Wallet found",
                Wallet = new UserWalletItem
                {
                    Id = wallet.Id,
                    UserId = wallet.UserId,
                    UserName = user.UserName,
                    Credit = wallet.Credit,
                    UsedCredit = wallet.UsedCredit,
                    RemainingCredit = wallet.RemainingCredit,
                    LastUpdated = wallet.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss")
                }
            };
        }
        catch (Exception ex)
        {
            return new UserWalletResponse { Status = false, Message = $"Error retrieving wallet: {ex.Message}" };
        }
    }
}