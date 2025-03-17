using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingCreateCommandReq(SocialSettingDto socialSettingDto) : IRequest<Unit>;