using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Setting.Queries;
using Parstech.Shop.Context.Application.DTOs.SiteSetting;
using Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.SiteSeting.Handlers.Queries;

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
        var setting = DapperHelper.ExecuteCommand<SettingDto>(_connectionSting, conn => conn.Query<SettingDto>(_settingQuery.GetSiteSetting).FirstOrDefault());
        result.Setting = setting;
        return result;
    }
}