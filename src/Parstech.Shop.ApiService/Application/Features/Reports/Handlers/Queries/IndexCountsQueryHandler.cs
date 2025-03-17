using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Reports.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Reports.Handlers.Queries;

public class IndexCountsQueryHandler : IRequestHandler<IndexCountsQueryReq, IndexCountsDto>
{
    private readonly IReporstRepository _countsRepo;

    public IndexCountsQueryHandler(IReporstRepository countsRepo)
    {
        _countsRepo = countsRepo;
    }

    public async Task<IndexCountsDto> Handle(IndexCountsQueryReq request, CancellationToken cancellationToken)
    {
        return await _countsRepo.GetIndexCounts();
    }
}