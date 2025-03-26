using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Representation.Handlers.Commands;

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