using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.Features.Categury.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Representation;
using Shop.Application.Features.Representation.Requests.Commands;

namespace Shop.Application.Features.Representation.Handlers.Commands
{
   
    public class RepresentationReadCommandHandler : IRequestHandler<RepresentationReadCommandReq, RepresentationDto>
    {
        private IRepresentationRepository _representationRep;
        private IMapper _mapper;

        public RepresentationReadCommandHandler(IRepresentationRepository representationRep, IMapper mapper)
        {
            _representationRep = representationRep;
            _mapper = mapper;
        }
        public async Task<RepresentationDto> Handle(RepresentationReadCommandReq request, CancellationToken cancellationToken)
        {
            var item = await _representationRep.GetAsync(request.repId);
            return _mapper.Map<RepresentationDto>(item);
        }
    }
    public class RepresentationReadsCommandHandler : IRequestHandler<RepresentationReadsCommandReq, List<RepresentationDto>>
    {
        private IRepresentationRepository _representationRep;
        private IMapper _mapper;

        public RepresentationReadsCommandHandler(IRepresentationRepository representationRep, IMapper mapper)
        {
            _representationRep = representationRep;
            _mapper = mapper;
        }
        public async Task<List<RepresentationDto>> Handle(RepresentationReadsCommandReq request, CancellationToken cancellationToken)
        {
            var list = await _representationRep.GetAll();
            return _mapper.Map<List<RepresentationDto>>(list);
        }
    }
}
