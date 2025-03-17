using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Queries;

public class DiscountSectionsSelectQueryHandler : IRequestHandler<DiscountSectionsSelectQueryReq, List<SectionDto>>
{
    #region Constractor

    private readonly string _conectionString;

    public DiscountSectionsSelectQueryHandler(IConfiguration configuration)
    {
        _conectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    #endregion

    public async Task<List<SectionDto>> Handle(DiscountSectionsSelectQueryReq request,
        CancellationToken cancellationToken)
    {
        string? query = "SELECT s.Id,s.SectionName,s.SectionTypeId from dbo.Section as s where s.SectionTypeId=3 ";
        return DapperHelper.ExecuteCommand(_conectionString, conn => conn.Query<SectionDto>(query).ToList());
    }
}