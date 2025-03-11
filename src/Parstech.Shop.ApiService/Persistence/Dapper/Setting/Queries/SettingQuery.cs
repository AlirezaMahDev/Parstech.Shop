using Shop.Application.Dapper.Setting.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.Setting.Queries
{
    public class SettingQuery : ISettingQuery
    {
        public string GetSiteSetting => "SELECT* FROM SiteSetting";
    }
}
