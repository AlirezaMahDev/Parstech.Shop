using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.Dapper.Setting.Queries;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.Features.SiteSeting.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.SiteSeting.Handlers.Queries
{
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
            AllSettingAndSeoDto result = new AllSettingAndSeoDto();
            var setting = DapperHelper.ExecuteCommand<SettingDto>(_connectionSting, conn => conn.Query<SettingDto>(_settingQuery.GetSiteSetting).FirstOrDefault());
            result.Setting = setting;
            return result;
        }
    }
}
