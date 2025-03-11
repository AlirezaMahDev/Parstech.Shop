using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SectionDetail;

namespace Shop.Application.Features.SectionDetail.Requests.Commands
{
    public record SectionDetailUpdateCommandReq(SectionDetailDto SectionDetailDto) : IRequest<SectionDetailDto>;

}
