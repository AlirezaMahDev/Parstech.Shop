using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IDashboardAdminGrpcClient
{
    Task<IndexCountsDto> GetDashboardCountsAsync();
}