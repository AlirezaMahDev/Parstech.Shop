using Shop.Application.DTOs.Reports;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IReporstRepository : IGenericRepository<IndexCountsDto>
    {
        Task<IndexCountsDto> GetIndexCounts();
    }
}
