using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Ticket.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.Ticket.Handler.Queries;

public class TicketPagingQueryHandler : IRequestHandler<TicketPagingQueryReq, PagingDto>
{
    private readonly ITicketRepository _ticketRep;
    private readonly IMapper _mapper;
    private readonly ITicketStatusesRepository _ticketStatusesRep;

    public TicketPagingQueryHandler(ITicketRepository ticketRep,
        IMapper mapper,
        ITicketStatusesRepository ticketStatusesRep)
    {
        _ticketRep = ticketRep;
        _mapper = mapper;
        _ticketStatusesRep = ticketStatusesRep;
    }

    public async Task<PagingDto> Handle(TicketPagingQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.Ticket> tickets = await _ticketRep.GetAll();
        IList<TicketDto> TicketsDto = new List<TicketDto>();
        foreach (Shared.Models.Ticket ticket in tickets)
        {
            TicketDto? TicketDto = _mapper.Map<TicketDto>(ticket);
            TicketStatus? status = await _ticketStatusesRep.GetAsync(ticket.StatusId);
            TicketDto.StatusTitle = status.StatusTitle;
            TicketsDto.Add(TicketDto);
        }

        IQueryable<TicketDto> result = TicketsDto.AsQueryable();

        PagingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                p.TicketCaption.Contains(request.Parameter.Filter));
        }

        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;
    }
}