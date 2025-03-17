using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingReadCommandReq(int id) : IRequest<SocialSettingDto>;

public record SocialSettingListReadCommandReq() : IRequest<List<SocialSettingDto>>;