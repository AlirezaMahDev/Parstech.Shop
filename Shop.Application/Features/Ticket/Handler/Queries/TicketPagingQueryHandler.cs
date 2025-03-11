using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Ticket;
using Shop.Application.Features.Ticket.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Ticket.Handler.Queries
{
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

            var tickets = await _ticketRep.GetAll();
            IList<TicketDto> TicketsDto = new List<TicketDto>();
            foreach (var ticket in tickets)
            {
                var TicketDto = _mapper.Map<TicketDto>(ticket);
                var status = await _ticketStatusesRep.GetAsync(ticket.StatusId);
                TicketDto.StatusTitle = status.StatusTitle;
                TicketsDto.Add(TicketDto);
            }

            IQueryable<TicketDto> result = TicketsDto.AsQueryable();

            PagingDto response = new PagingDto();

            if (!string.IsNullOrEmpty(request.Parameter.Filter))
            {
                result = result.Where(p =>
                    (p.TicketCaption.Contains(request.Parameter.Filter)));
            }
            int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

            response.CurrentPage = request.Parameter.CurrentPage;
            int count = result.Count();
            response.PageCount = count / request.Parameter.TakePage;


            response.List = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

            return response;


        }
    }
}
