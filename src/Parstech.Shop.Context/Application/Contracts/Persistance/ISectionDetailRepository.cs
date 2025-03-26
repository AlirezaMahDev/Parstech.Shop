using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ISectionDetailRepository:IGenericRepository<SectionDetail>
{
    Task<List<SectionDetail>> GetDetailsOfSection(int SectionId);
}