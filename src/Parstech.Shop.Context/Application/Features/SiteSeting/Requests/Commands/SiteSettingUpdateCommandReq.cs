using MediatR;

using Parstech.Shop.Context.Application.DTOs.SiteSetting;

namespace Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Commands;

public record SiteSettingUpdateCommandReq(int id, SiteDto SiteDto) : IRequest<Unit>;
public record SeoSettingUpdateCommandReq(int id, SeoDto SeoDto) : IRequest<Unit>;