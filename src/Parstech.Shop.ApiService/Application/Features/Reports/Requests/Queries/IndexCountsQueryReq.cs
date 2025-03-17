using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Reports.Requests.Queries;

public record IndexCountsQueryReq() : IRequest<IndexCountsDto>;