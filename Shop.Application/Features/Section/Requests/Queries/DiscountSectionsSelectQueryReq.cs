using MediatR;
using Shop.Application.DTOs.Section;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Section.Requests.Queries
{
    public record DiscountSectionsSelectQueryReq():IRequest<List<SectionDto>>;

}
