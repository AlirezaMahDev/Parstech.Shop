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
    public class RahkaranOrderRepository:GenericRepository<RahkaranOrder>,IRahkaranOrderRepository
    {
        private readonly DatabaseContext _context;
        public RahkaranOrderRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
