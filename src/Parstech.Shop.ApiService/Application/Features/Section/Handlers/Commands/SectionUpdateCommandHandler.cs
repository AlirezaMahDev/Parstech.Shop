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
using Shop.Application.Features.SocialSetting.Requests.Commands;
using Shop.Domain.Models;

namespace Shop.Application.Features.Section.Handlers.Commands
{
    public class SectionUpdateCommandHandler : IRequestHandler<SectionUpdateCommandReq, SectionDto>
    {
        private ISectionRepository _sectionRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public SectionUpdateCommandHandler(ISectionRepository sectionRep, IMapper mapper, IMediator madiiator)
        {
            _sectionRep = sectionRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<SectionDto> Handle(SectionUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var section = await _sectionRep.GetAsync(request.SectionDto.Id);
            _mapper.Map(request.SectionDto, section);
            await _sectionRep.UpdateAsync(section);
            var result = await _madiiator.Send(new SectionReadCommandReq(section.Id));
            return result;
        }
    }
}
