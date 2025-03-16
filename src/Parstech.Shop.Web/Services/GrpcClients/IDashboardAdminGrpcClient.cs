using Shop.Application.DTOs.Reports;
using System.Threading.Tasks;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IDashboardAdminGrpcClient
    {
        Task<IndexCountsDto> GetDashboardCountsAsync();
    }
} 