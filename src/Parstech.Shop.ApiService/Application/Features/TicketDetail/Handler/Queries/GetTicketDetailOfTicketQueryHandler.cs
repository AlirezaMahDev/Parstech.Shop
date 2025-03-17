using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.TicketDetail.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.TicketDetail.Handler.Queries;

public class
    GetTicketDetailOfTicketQueryHandler : IRequestHandler<GetTicketDetailOfTicketQueryReq, IQueryable<TicketDetailsDto>>
{
    private readonly ITicketDetailRepository _ticketDetailRep;

    public GetTicketDetailOfTicketQueryHandler(ITicketDetailRepository ticketDetailRep)
    {
        _ticketDetailRep = ticketDetailRep;
    }

    public async Task<IQueryable<TicketDetailsDto>> Handle(GetTicketDetailOfTicketQueryReq request,
        CancellationToken cancellationToken)
    {
        return await _ticketDetailRep.GetTicketDetailOfTicketWithTypeTitle(request.ticketId);
    }
}