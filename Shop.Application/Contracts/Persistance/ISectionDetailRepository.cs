using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface ISectionDetailRepository:IGenericRepository<SectionDetail>
    {
        Task<List<SectionDetail>> GetDetailsOfSection(int SectionId);
    }
}
