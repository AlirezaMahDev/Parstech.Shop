using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.SocialSetting;

namespace Shop.Application.Features.SocialSetting.Requests.Commands
{
    public record SocialSettingCreateCommandReq(SocialSettingDto socialSettingDto) : IRequest<Unit>;
}
