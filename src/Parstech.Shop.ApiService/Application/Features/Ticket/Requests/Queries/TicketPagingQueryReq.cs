using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Ticket.Requests.Queries;

public record TicketPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;