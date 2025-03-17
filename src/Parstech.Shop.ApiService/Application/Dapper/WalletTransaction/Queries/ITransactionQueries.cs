namespace Parstech.Shop.ApiService.Application.Dapper.WalletTransaction.Queries;

public interface ITransactionQueries
{
    string GetAllTransaction { get; }
    string GetActiveCredit { get; }
}