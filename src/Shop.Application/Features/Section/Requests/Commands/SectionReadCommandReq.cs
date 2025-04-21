using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Section;

namespace Shop.Application.Features.Section.Requests.Commands
{
    public record SectionReadCommandReq(int id) : IRequest<SectionDto>;
    
}
