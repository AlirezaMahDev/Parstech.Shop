using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingUpdateCommandReq(SocialSettingDto socialSettingDto) : IRequest<SocialSettingDto>;