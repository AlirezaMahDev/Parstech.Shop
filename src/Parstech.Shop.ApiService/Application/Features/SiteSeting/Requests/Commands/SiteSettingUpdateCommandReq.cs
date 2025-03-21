﻿using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Commands;

public record SiteSettingUpdateCommandReq(int id, SiteDto SiteDto) : IRequest<Unit>;

public record SeoSettingUpdateCommandReq(int id, SeoDto SeoDto) : IRequest<Unit>;