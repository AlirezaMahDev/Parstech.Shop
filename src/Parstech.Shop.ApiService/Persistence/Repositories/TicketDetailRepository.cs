using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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
        List<TicketDetail>? ticketDetails =
            await _context.TicketDetails.Where(z => z.TicketId == ticketId).ToListAsync();
        IList<TicketDetailsDto> TicketDetailsDto = new List<TicketDetailsDto>();
        foreach (TicketDetail ticketDetail in ticketDetails)
        {
            TicketDetailsDto? ticketDetailDto = _mapper.Map<TicketDetailsDto>(ticketDetail);
            TicketType? type = await _context.TicketTypes.FirstOrDefaultAsync(z => z.TypeId == ticketDetailDto.TypeId);
            ticketDetailDto.TypeTitle = type.TypeTitle;
            TicketDetailsDto.Add(ticketDetailDto);
        }

        return TicketDetailsDto.AsQueryable();
    }
}