using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Reports.Requests.Queries;

public record IndexCountsQueryReq() : IRequest<IndexCountsDto>;