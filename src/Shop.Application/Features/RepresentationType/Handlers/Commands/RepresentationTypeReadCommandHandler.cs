using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Representation;
using Shop.Application.Features.Representation.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.RepresentationType;
using Shop.Application.Features.RepresentationType.Requests.Commands;

namespace Shop.Application.Features.RepresentationType.Handlers.Commands
{
    public class RepresentationTypeReadCommandHandler : IRequestHandler<RepresentationTypeReadCommandReq, RepresentationTypeDto>
    {
        private IRepresentationTypeRepository _representationTypeRep;
        private IMapper _mapper;

        public RepresentationTypeReadCommandHandler(IRepresentationTypeRepository representationTypeRep, IMapper mapper)
        {
            _representationTypeRep = representationTypeRep;
            _mapper = mapper;
        }
        public async Task<RepresentationTypeDto> Handle(RepresentationTypeReadCommandReq request, CancellationToken cancellationToken)
        {
            var item = await _representationTypeRep.GetAsync(request.RepTypeId);
            return _mapper.Map<RepresentationTypeDto>(item);
        }
    }
    
    public class RepresentationTypeReadsCommandHandler : IRequestHandler<RepresentationTypeReadsCommandReq, List<RepresentationTypeDto>>
    {
        private IRepresentationTypeRepository _representationTypeRep;
        private IMapper _mapper;

        public RepresentationTypeReadsCommandHandler(IRepresentationTypeRepository representationTypeRep, IMapper mapper)
        {
            _representationTypeRep = representationTypeRep;
            _mapper = mapper;
        }
        public async Task<List<RepresentationTypeDto>> Handle(RepresentationTypeReadsCommandReq request, CancellationToken cancellationToken)
        {
            var list = await _representationTypeRep.GetAll();
            return _mapper.Map<List<RepresentationTypeDto>>(list);
        }
    }
}
