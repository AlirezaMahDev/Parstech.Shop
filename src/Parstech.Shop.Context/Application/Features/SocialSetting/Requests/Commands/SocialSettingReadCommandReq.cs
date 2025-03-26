using MediatR;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;

namespace Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;

public record SocialSettingReadCommandReq(int id) : IRequest<SocialSettingDto>;
public record SocialSettingListReadCommandReq() : IRequest<List<SocialSettingDto>>;