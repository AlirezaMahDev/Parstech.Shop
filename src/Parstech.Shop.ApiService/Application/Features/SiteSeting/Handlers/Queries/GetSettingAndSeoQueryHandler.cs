using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Setting.Queries;
using Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Handlers.Queries;

public class GetSettingAndSeoQueryHandler : IRequestHandler<GetSettingAndSeoQueryReq, AllSettingAndSeoDto>
{
    private readonly ISettingQuery _settingQuery;
    private readonly string _connectionSting;

    public GetSettingAndSeoQueryHandler(ISettingQuery settingQuery, IConfiguration configuration)
    {
        _settingQuery = settingQuery;
        _connectionSting = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<AllSettingAndSeoDto> Handle(GetSettingAndSeoQueryReq request, CancellationToken cancellationToken)
    {
        AllSettingAndSeoDto result = new();
        SettingDto setting = DapperHelper.ExecuteCommand<SettingDto>(_connectionSting,
            conn => conn.Query<SettingDto>(_settingQuery.GetSiteSetting).FirstOrDefault());
        result.Setting = setting;
        return result;
    }
}