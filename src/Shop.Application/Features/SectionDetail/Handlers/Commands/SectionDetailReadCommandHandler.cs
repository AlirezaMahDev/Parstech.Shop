using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SectionDetail;
using Shop.Application.DTOs.SocialSetting;
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Features.SectionDetail.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.SectionDetail.Handlers.Commands
{
    

    public class SectionDetailReadCommandHandler : IRequestHandler<SectionDetailReadCommandReq, SectionDetailDto>
    {


        private ISectionDetailRepository _sectionDetailRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public SectionDetailReadCommandHandler(ISectionDetailRepository sectionDetailRep, IMapper mapper, IMediator madiiator)
        {
            _sectionDetailRep = sectionDetailRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<SectionDetailDto> Handle(SectionDetailReadCommandReq request, CancellationToken cancellationToken)
        {
            var section = await _sectionDetailRep.GetAsync(request.id);
            return _mapper.Map<SectionDetailDto>(section);
        }
    }
}
