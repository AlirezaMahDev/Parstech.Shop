using MediatR;
using Shop.Application.DTOs.TicketDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.TicketDetail.Requests.Queries
{
    public record GetTicketDetailOfTicketQueryReq(int ticketId) : IRequest<IQueryable<TicketDetailsDto>>;
}
