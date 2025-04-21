using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Ticket;
using Shop.Application.DTOs.Wallet;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        public TicketRepository(DatabaseContext context,
            IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
