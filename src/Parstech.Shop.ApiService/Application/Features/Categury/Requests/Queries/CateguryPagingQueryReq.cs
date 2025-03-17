using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

public record CateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;