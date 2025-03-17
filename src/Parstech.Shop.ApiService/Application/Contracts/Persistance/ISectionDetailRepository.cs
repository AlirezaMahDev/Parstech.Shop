using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ISectionDetailRepository : IGenericRepository<SectionDetail>
{
    Task<List<SectionDetail>> GetDetailsOfSection(int SectionId);
}