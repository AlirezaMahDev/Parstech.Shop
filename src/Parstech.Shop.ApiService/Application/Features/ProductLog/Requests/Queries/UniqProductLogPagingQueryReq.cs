using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Queries;

public record UniqProductLogPagingQueryReq(ParameterLogDto parameter) : IRequest<PagingDto>;