using MediatR;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;

namespace Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingUpdateCommandReq(SocialSettingDto socialSettingDto) : IRequest<SocialSettingDto>;