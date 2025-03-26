using MediatR;
using Parstech.Shop.Context.Application.DTOs.TicketDetails;

namespace Parstech.Shop.Context.Application.Features.TicketDetail.Requests.Queries;

public record GetTicketDetailOfTicketQueryReq(int ticketId) : IRequest<IQueryable<TicketDetailsDto>>;