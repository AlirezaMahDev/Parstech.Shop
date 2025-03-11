using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.SiteSetting;
using Shop.Domain.Models;

namespace Shop.Application.Features.SiteSeting.Requests.Commands
{

    public record SiteSettingUpdateCommandReq(int id, SiteDto SiteDto) : IRequest<Unit>;
    public record SeoSettingUpdateCommandReq(int id, SeoDto SeoDto) : IRequest<Unit>;

}
