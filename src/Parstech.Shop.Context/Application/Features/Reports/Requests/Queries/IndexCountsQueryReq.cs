using MediatR;

using Parstech.Shop.Context.Application.DTOs.Reports;

namespace Parstech.Shop.Context.Application.Features.Reports.Requests.Queries;

public record IndexCountsQueryReq() : IRequest<IndexCountsDto>;