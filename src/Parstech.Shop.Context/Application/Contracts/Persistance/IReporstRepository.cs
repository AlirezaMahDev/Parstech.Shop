using Parstech.Shop.Context.Application.DTOs.Reports;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IReporstRepository : IGenericRepository<IndexCountsDto>
{
    Task<IndexCountsDto> GetIndexCounts();
}