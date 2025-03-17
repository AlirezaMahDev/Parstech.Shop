using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.TicketDetail.Requests.Queries;

public record GetTicketDetailOfTicketQueryReq(int ticketId) : IRequest<IQueryable<TicketDetailsDto>>;