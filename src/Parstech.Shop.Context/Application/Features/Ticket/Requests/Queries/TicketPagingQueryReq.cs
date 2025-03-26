using MediatR;

using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Ticket.Requests.Queries;

public record TicketPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;