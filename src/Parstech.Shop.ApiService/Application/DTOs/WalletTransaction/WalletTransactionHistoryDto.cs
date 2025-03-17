using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.DTOs.WalletTransaction;

public class WalletTransactionHistoryDto
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public List<WalletTransactionDto> Transactions { get; set; } = new List<WalletTransactionDto>();
} 