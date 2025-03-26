using Parstech.Shop.Context.Application.Dapper.Setting.Queries;

namespace Parstech.Shop.Context.Persistence.Dapper.Setting.Queries;

public class SettingQuery : ISettingQuery
{
    public string GetSiteSetting => "SELECT* FROM SiteSetting";
}