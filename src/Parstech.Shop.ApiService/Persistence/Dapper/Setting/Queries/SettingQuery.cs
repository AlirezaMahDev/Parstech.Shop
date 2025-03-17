using Parstech.Shop.ApiService.Application.Dapper.Setting.Queries;

namespace Parstech.Shop.ApiService.Persistence.Dapper.Setting.Queries;

public class SettingQuery : ISettingQuery
{
    public string GetSiteSetting => "SELECT* FROM SiteSetting";
}