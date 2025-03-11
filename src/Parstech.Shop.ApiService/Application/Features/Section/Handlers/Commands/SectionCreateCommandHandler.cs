using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Section;
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Generator;

namespace Shop.Application.Features.Section.Handlers.Commands
{
    public class SectionCreateCommandHandler : IRequestHandler<SectionCreateCommandReq, SectionDto>
    {
        private ISectionRepository _sectionRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public SectionCreateCommandHandler(ISectionRepository sectionRep, IMapper mapper, IMediator madiiator)
        {
            _sectionRep = sectionRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }
        public async Task<SectionDto> Handle(SectionCreateCommandReq request, CancellationToken cancellationToken)
        {
            var section = _mapper.Map<Domain.Models.Section>(request.SectionDto);
            await _sectionRep.AddAsync(section);
            var result = await _madiiator.Send(new SectionReadCommandReq(section.Id));
            return result;
        }
    }
}
