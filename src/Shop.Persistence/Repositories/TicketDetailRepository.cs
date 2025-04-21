using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Ticket;
using Shop.Application.DTOs.TicketDetails;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class TicketDetailRepository : GenericRepository<TicketDetail>, ITicketDetailRepository
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        public TicketDetailRepository(DatabaseContext context,
            IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IQueryable<TicketDetailsDto>> GetTicketDetailOfTicketWithTypeTitle(int ticketId)
        {
            var ticketDetails = await _context.TicketDetails.Where(z => z.TicketId == ticketId).ToListAsync();
            IList<TicketDetailsDto> TicketDetailsDto = new List<TicketDetailsDto>();
            foreach(var ticketDetail in ticketDetails)
            {
                var ticketDetailDto = _mapper.Map<TicketDetailsDto>(ticketDetail);
                var type = await _context.TicketTypes.FirstOrDefaultAsync(z => z.TypeId == ticketDetailDto.TypeId);
                ticketDetailDto.TypeTitle = type.TypeTitle;
                TicketDetailsDto.Add(ticketDetailDto);
            }
            return TicketDetailsDto.AsQueryable();
        }
    }
}
