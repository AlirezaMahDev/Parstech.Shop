using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.DashboardAdmin;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Reports;
using Shop.Application.Features.Counts.Requests.Queries;
using System.Threading.Tasks;

namespace Parstech.Shop.ApiService.Services.GrpcServices
{
    public class DashboardAdminGrpcService : DashboardAdminService.DashboardAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IReporstRepository _reportsRepository;

        public DashboardAdminGrpcService(IMediator mediator, IReporstRepository reportsRepository)
        {
            _mediator = mediator;
            _reportsRepository = reportsRepository;
        }

        public override async Task<IndexCountsDto> GetDashboardCounts(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                // Get dashboard data using the repository directly
                var data = await _reportsRepository.GetIndexCounts();

                // Map to proto DTO
                var result = new IndexCountsDto
                {
                    Time = data.Time ?? string.Empty,
                    UserCount = data.UserCount,
                    ProductCount = data.ProductCount,
                    IsLoadOrderCount = data.IsLoadOrderCount,
                    AllTransactionsCount = data.AllTransactionsCount,
                    CoinTransactionsCount = data.CoinTransactionsCount,
                    WalletTransactionsCount = data.WalletTransactionsCount,
                    FacilitiesTransactionsCount = data.FacilitiesTransactionsCount,
                    PishFactorCount = data.PishFactorCount,
                    SouratHesabCount = data.SouratHesabCount
                };

                // Add representation products for chart
                if (data.RepresentationsProductsForChart != null)
                {
                    foreach (var item in data.RepresentationsProductsForChart)
                    {
                        result.RepresentationsProductsForChart.Add(new RepresentationsProducts
                        {
                            RepresentationName = item.RepresentationName ?? string.Empty,
                            RepresentationProducts = item.RepresentationProducts
                        });
                    }
                }

                // Add representation products for map
                if (data.RepresentationsProductsForMap != null)
                {
                    foreach (var item in data.RepresentationsProductsForMap)
                    {
                        result.RepresentationsProductsForMap.Add(new RepresentationsSells
                        {
                            RepresentationName = item.RepresentationName ?? string.Empty,
                            Latitude = item.latitude,
                            Longitude = item.longitude,
                            RepresentationSells = item.RepresentationSells
                        });
                    }
                }

                return result;
            }
            catch (System.Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"An error occurred: {ex.Message}"));
            }
        }
    }
} 