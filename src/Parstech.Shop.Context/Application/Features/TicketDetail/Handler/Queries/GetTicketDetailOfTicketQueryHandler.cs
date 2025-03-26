using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.TicketDetails;
using Parstech.Shop.Context.Application.Features.TicketDetail.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.TicketDetail.Handler.Queries;

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