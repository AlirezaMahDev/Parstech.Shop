using Microsoft.Data.SqlClient;

namespace Parstech.Shop.Context.Application.Dapper.Helper;

public static class DapperHelper
{
    #region  Helpers

    public static void ExecuteCommand(string connection, Action<SqlConnection> task)
    {
        using (var conn = new SqlConnection(connection))
        {
            conn.Open();
            task(conn);
        }
    }

    public static T ExecuteCommand<T>(string connection, Func<SqlConnection, T> task)
    {
        using (var conn = new SqlConnection(connection))
        {
            conn.Open();
            return task(conn);
        }
    }

    #endregion
}