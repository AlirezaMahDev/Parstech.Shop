using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Queries;

public record PropertyCateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;