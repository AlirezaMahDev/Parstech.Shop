using Microsoft.Data.SqlClient;

namespace Parstech.Shop.ApiService.Application.Dapper.Helper;

public static class DapperHelper
{
    #region Helpers

    public static void ExecuteCommand(string connection, Action<SqlConnection> task)
    {
        using (SqlConnection conn = new(connection))
        {
            conn.Open();
            task(conn);
        }
    }

    public static T ExecuteCommand<T>(string connection, Func<SqlConnection, T> task)
    {
        using (SqlConnection conn = new(connection))
        {
            conn.Open();
            return task(conn);
        }
    }

    #endregion
}