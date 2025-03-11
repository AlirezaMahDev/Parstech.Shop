using MediatR;
using Shop.Application.DTOs.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.SiteSeting.Requests.Queries
{
    public record GetSettingAndSeoQueryReq():IRequest<AllSettingAndSeoDto>;

}
