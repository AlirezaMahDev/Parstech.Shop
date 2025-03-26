using Parstech.Shop.Context.Application.Dapper.User.Queries;

namespace Parstech.Shop.Context.Persistence.Dapper.User.Queries;

public class UserQueries : IUserQueries
{
    public string GetAllUsers => "SELECT dbo.[User].UserName, dbo.UserBilling.FirstName, dbo.UserBilling.LastName FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId";
}