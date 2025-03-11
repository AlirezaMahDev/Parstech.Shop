using Shop.Application.Dapper.User.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.User.Queries
{
    public class UserQueries : IUserQueries
    {
        public string GetAllUsers => "SELECT dbo.[User].UserName, dbo.UserBilling.FirstName, dbo.UserBilling.LastName FROM dbo.[User] INNER JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId";
    }
}
