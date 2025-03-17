using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record SiteMapGenerateQueryReq() : IRequest<List<SitemapDto>>;