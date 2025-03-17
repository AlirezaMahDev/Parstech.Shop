using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IReporstRepository : IGenericRepository<IndexCountsDto>
{
    Task<IndexCountsDto> GetIndexCounts();
}