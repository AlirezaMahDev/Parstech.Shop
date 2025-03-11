using MediatR;
using Shop.Application.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Ticket.Requests.Queries
{
    public record TicketPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;

}
