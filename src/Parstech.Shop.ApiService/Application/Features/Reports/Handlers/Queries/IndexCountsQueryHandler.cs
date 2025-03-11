using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Reports;
using Shop.Application.DTOs.CouponType;
using Shop.Application.Features.Counts.Requests.Queries;
using Shop.Application.Features.CouponType.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Counts.Handlers.Queries
{
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
}
