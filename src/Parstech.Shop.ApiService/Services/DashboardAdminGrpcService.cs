using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

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
            IndexCountsDto data = await _reportsRepository.GetIndexCounts();

            // Map to proto DTO
            IndexCountsDto result = new()
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
                foreach (RepresentationsProducts item in data.RepresentationsProductsForChart)
                {
                    result.RepresentationsProductsForChart.Add(new()
                    {
                        RepresentationName = item.RepresentationName ?? string.Empty,
                        RepresentationProducts = item.RepresentationProducts
                    });
                }
            }

            // Add representation products for map
            if (data.RepresentationsProductsForMap != null)
            {
                foreach (RepresentationsSells item in data.RepresentationsProductsForMap)
                {
                    result.RepresentationsProductsForMap.Add(new()
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
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }
}