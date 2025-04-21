using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface ISectionRepository:IGenericRepository<Section>
    {
        Task<bool> CheckSectionDetailExist(int SectionId);
        Task<Section> GetByOlaviat(int Olaviat);
        Task<Section> GetByStore(int storeId);
        Task<List<Section>> GetSectionsOfStore(int? storeId);
    }
}
