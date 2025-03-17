using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.TicketDetail.Requests.Queries;

public record GetTicketDetailOfTicketQueryReq(int ticketId) : IRequest<IQueryable<TicketDetailsDto>>;