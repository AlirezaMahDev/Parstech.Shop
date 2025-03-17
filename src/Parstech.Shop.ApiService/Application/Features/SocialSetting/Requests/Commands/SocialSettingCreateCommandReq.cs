using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingCreateCommandReq(SocialSettingDto socialSettingDto) : IRequest<Unit>;