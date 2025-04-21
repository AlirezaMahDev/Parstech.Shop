using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Section;
using Shop.Application.Features.Section.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Section.Handlers.Queries
{
    public class DiscountSectionsSelectQueryHandler : IRequestHandler<DiscountSectionsSelectQueryReq, List<SectionDto>>
    {
        #region Constractor
        private readonly string _conectionString;

        public DiscountSectionsSelectQueryHandler(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("DatabaseConnection");
        }

        #endregion
        public async Task<List<SectionDto>> Handle(DiscountSectionsSelectQueryReq request, CancellationToken cancellationToken)
        {
            var query = "SELECT s.Id,s.SectionName,s.SectionTypeId from dbo.Section as s where s.SectionTypeId=3 ";
            return DapperHelper.ExecuteCommand<List<SectionDto>>(_conectionString, conn => conn.Query<SectionDto>(query).ToList());
        }
    }
}
