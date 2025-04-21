using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SocialSetting;
using Shop.Application.Features.Section.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Section.Handlers.Commands
{
    

    public class SectionReadCommandHandler : IRequestHandler<SectionReadCommandReq, SectionDto>
    {


        private ISectionRepository _sectionRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public SectionReadCommandHandler(ISectionRepository sectionRep, IMapper mapper, IMediator madiiator)
        {
            _sectionRep = sectionRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<SectionDto> Handle(SectionReadCommandReq request, CancellationToken cancellationToken)
        {
            var section = await _sectionRep.GetAsync(request.id);
            return _mapper.Map<SectionDto>(section);
        }
    }
}
