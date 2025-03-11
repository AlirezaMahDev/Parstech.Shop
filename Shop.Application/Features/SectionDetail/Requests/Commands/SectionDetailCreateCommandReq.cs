using MediatR;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SectionDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.SectionDetail.Requests.Commands
{

    public record SectionDetailCreateCommandReq(SectionDetailDto SectionDetailDto) : IRequest<SectionDetailDto>;
}
