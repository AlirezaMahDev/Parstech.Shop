using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.TicketDetails;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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