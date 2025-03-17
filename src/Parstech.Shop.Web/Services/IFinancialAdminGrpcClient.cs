using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IFinancialAdminGrpcClient
{
    // Wallet operations
    Task<PagingDto> GetWalletsPagingAsync(ParameterDto parameter);
    Task<List<UserFilterDto>> GetUserFiltersAsync();
    Task<bool> BlockOrUnblockWalletAsync(bool isBlocked, int walletId);

    // Wallet transactions operations
    Task<PagingDto> GetWalletTransactionsPagingAsync(WalletTransactionParameterDto parameter);
    Task<ResponseDto> CreateWalletTransactionAsync(WalletTransactionDto transaction);
    Task<WalletTransactionDto> GetWalletTransactionDetailAsync(int transactionId);

    // Installment operations
    Task<ResponseDto> PayInstallmentAsync(int transactionId);

    // Facility operations
    Task<ResponseDto> CreateFacilitiesAsync(FacilitiesDto facilities);
    Task<ResponseDto> RegisterFacilitiesByExcelAsync(string type, IFormFile file);
    Task<ResponseDto> ProcessFacilityPaymentsByExcelAsync(IFormFile file);
}