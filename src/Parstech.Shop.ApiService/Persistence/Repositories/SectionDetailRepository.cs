using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class SectionDetailRepository:GenericRepository<SectionDetail>,ISectionDetailRepository
    {
        private readonly DatabaseContext _context;

        public SectionDetailRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SectionDetail>> GetDetailsOfSection(int SectionId)
        {
           return await _context.SectionDetails.Where(u => u.SectionId == SectionId).ToListAsync();
        }
    }
}
