using MediatR;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;

namespace Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingCreateCommandReq(SocialSettingDto socialSettingDto) : IRequest<Unit>;