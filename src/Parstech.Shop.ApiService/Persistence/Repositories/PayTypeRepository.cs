using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class PayTypeRepository:GenericRepository<PayType>, IPayTypeRepository
    {
        private readonly DatabaseContext _context;
        public PayTypeRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<PayType>> GetActiveList()
        {
            return _context.PayTypes.Where(x => x.IsActive).ToList();
        }
    }
}
