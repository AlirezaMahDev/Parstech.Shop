using Parstech.Shop.ApiService.Application.Dapper.WalletTransaction.Queries;

namespace Parstech.Shop.ApiService.Persistence.Dapper.WalletTransaction.Queries;

public class TransactionQueries : ITransactionQueries
{
    public string GetAllTransaction =>
        "SELECT dbo.[User].UserName, dbo.UserBilling.FirstName, dbo.UserBilling.LastName, dbo.Wallets.Amount, dbo.Wallets.Fecilities, dbo.Wallets.OrgCredit, dbo.WalletTransaction.Price, dbo.WalletTransaction.Type, dbo.WalletTransaction.TypeId,  dbo.WalletTypes.TypeTitle, dbo.WalletTypes.Color, dbo.WalletTransaction.Description, dbo.WalletTransaction.CreateDate, dbo.WalletTransaction.TrackingCode, dbo.WalletTransaction.Start, dbo.WalletTransaction.Active,  dbo.WalletTransaction.Persent, dbo.WalletTransaction.ParentFecilitiesId, dbo.WalletTransaction.ExpireDate, dbo.WalletTransaction.Id FROM dbo.[User] INNER JOIN dbo.Wallets ON dbo.[User].Id = dbo.Wallets.UserId INNER JOIN dbo.WalletTransaction ON dbo.Wallets.WalletId = dbo.WalletTransaction.WalletId INNER JOIN dbo.WalletTypes ON dbo.WalletTransaction.TypeId = dbo.WalletTypes.TypeId INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId where dbo.WalletTransaction.Type!='Coin' Order By dbo.WalletTransaction.Id Desc";

    public string GetActiveCredit =>
        "SELECT dbo.WalletTransaction.Id,dbo.WalletTransaction.Price, dbo.WalletTransaction.Type, dbo.WalletTransaction.TypeId, dbo.WalletTransaction.TrackingCode, dbo.WalletTransaction.Description, dbo.WalletTransaction.CreateDate, dbo.WalletTransaction.ExpireDate,dbo.WalletTransaction.Persent, dbo.WalletTransaction.Month, dbo.WalletTransaction.Start, dbo.WalletTransaction.Active, dbo.WalletTransaction.ParentFecilitiesId, dbo.Wallets.WalletId, dbo.Wallets.UserId, dbo.[User].UserName,dbo.UserBilling.FirstName, dbo.UserBilling.LastName FROM dbo.[User] INNER JOIN dbo.Wallets ON dbo.[User].Id = dbo.Wallets.UserId INNER JOIN dbo.WalletTransaction ON dbo.Wallets.WalletId = dbo.WalletTransaction.WalletId INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId  where dbo.WalletTransaction.Start=1";
}