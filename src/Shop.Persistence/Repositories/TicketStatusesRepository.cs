using AutoMapper;
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
    public class TicketStatusesRepository : GenericRepository<TicketStatus>, ITicketStatusesRepository
    {
        private DatabaseContext _context;
        public TicketStatusesRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
