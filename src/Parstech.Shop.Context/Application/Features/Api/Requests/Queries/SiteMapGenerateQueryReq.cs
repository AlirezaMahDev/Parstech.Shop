using MediatR;

using Parstech.Shop.Context.Application.DTOs.SiteMap;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record SiteMapGenerateQueryReq():IRequest<List<SitemapDto>>;