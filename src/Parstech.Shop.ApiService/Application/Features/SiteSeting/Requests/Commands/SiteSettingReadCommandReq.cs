using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Commands;

public record SiteSettingReadCommandReq(int id) : IRequest<SiteDto>;

public record SeoSettingReadCommandReq(int id) : IRequest<SeoDto>;