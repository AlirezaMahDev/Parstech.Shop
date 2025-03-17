using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingUpdateCommandReq(SocialSettingDto socialSettingDto) : IRequest<SocialSettingDto>;