using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.TicketDetails;
using Shop.Application.Features.TicketDetail.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.TicketDetail.Handler.Queries
{
    public class GetTicketDetailOfTicketQueryHandler : IRequestHandler<GetTicketDetailOfTicketQueryReq, IQueryable<TicketDetailsDto>>
    {
        private readonly ITicketDetailRepository _ticketDetailRep;

        public GetTicketDetailOfTicketQueryHandler(ITicketDetailRepository ticketDetailRep)
        {
            _ticketDetailRep = ticketDetailRep;
        }

        public async Task<IQueryable<TicketDetailsDto>> Handle(GetTicketDetailOfTicketQueryReq request, CancellationToken cancellationToken)
        {
            return await _ticketDetailRep.GetTicketDetailOfTicketWithTypeTitle(request.ticketId);
        }
    }
}
