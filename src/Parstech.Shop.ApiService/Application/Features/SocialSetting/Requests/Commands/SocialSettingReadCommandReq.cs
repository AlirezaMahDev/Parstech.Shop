using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingReadCommandReq(int id) : IRequest<SocialSettingDto>;

public record SocialSettingListReadCommandReq() : IRequest<List<SocialSettingDto>>;