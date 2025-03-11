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
    public class RahkaranUserRepository : GenericRepository<RahkaranUser>, IRahkaranUserRepository
    {
        private readonly DatabaseContext _context;
        public RahkaranUserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
