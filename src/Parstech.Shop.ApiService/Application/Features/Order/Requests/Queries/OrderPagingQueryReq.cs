using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record OrderPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;