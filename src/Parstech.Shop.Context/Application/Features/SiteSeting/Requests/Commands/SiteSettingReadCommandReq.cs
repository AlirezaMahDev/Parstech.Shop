using MediatR;

using Parstech.Shop.Context.Application.DTOs.SiteSetting;

namespace Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Commands;

public record SiteSettingReadCommandReq(int id) : IRequest<SiteDto>;

public record SeoSettingReadCommandReq(int id) : IRequest<SeoDto>;