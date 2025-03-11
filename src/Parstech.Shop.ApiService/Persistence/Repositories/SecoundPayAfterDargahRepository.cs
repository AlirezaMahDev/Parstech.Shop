using Microsoft.EntityFrameworkCore;
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
    public class SecoundPayAfterDargahRepository:GenericRepository<SecoundPayAfterDargah>,ISecoundPayAfterDargahRepository
    {
        private readonly DatabaseContext _context;
        public SecoundPayAfterDargahRepository(DatabaseContext context):base(context) 
        {
            _context=context;
        }

        public async Task<SecoundPayAfterDargah> GetByOrderId(int orderId)
        {
           return await _context.SecoundPayAfterDargahs.FirstOrDefaultAsync(u=>u.OrderId==orderId);
        }
    }
}
