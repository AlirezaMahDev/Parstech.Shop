using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;
using Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.RepresentationType.Handlers.Commands;

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