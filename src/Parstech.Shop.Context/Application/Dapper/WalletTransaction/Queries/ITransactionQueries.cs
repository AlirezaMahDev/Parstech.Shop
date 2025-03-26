namespace Parstech.Shop.Context.Application.Dapper.WalletTransaction.Queries;

public interface ITransactionQueries
{
    string GetAllTransaction { get; }
    string GetActiveCredit { get; }
}