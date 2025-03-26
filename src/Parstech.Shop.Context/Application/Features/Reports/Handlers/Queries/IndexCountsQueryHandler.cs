using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Reports;
using Parstech.Shop.Context.Application.Features.Reports.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Reports.Handlers.Queries;

public class IndexCountsQueryHandler : IRequestHandler<IndexCountsQueryReq, IndexCountsDto>
{
    private readonly IReporstRepository _countsRepo;
    public IndexCountsQueryHandler (IReporstRepository countsRepo)
    {
        _countsRepo = countsRepo;
    }
    public async Task<IndexCountsDto> Handle(IndexCountsQueryReq request, CancellationToken cancellationToken)
    {
        return await _countsRepo.GetIndexCounts();
    }
}