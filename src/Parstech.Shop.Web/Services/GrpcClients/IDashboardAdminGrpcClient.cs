using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public interface IDashboardAdminGrpcClient
{
    Task<IndexCountsDto> GetDashboardCountsAsync();
}