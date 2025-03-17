using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.RepresentationType.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.RepresentationType.Handlers.Commands;

public class
    RepresentationTypeReadCommandHandler : IRequestHandler<RepresentationTypeReadCommandReq, RepresentationTypeDto>
{
    private IRepresentationTypeRepository _representationTypeRep;
    private IMapper _mapper;

    public RepresentationTypeReadCommandHandler(IRepresentationTypeRepository representationTypeRep, IMapper mapper)
    {
        _representationTypeRep = representationTypeRep;
        _mapper = mapper;
    }

    public async Task<RepresentationTypeDto> Handle(RepresentationTypeReadCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.RepresentationType? item = await _representationTypeRep.GetAsync(request.RepTypeId);
        return _mapper.Map<RepresentationTypeDto>(item);
    }
}

public class
    RepresentationTypeReadsCommandHandler : IRequestHandler<RepresentationTypeReadsCommandReq,
    List<RepresentationTypeDto>>
{
    private IRepresentationTypeRepository _representationTypeRep;
    private IMapper _mapper;

    public RepresentationTypeReadsCommandHandler(IRepresentationTypeRepository representationTypeRep, IMapper mapper)
    {
        _representationTypeRep = representationTypeRep;
        _mapper = mapper;
    }

    public async Task<List<RepresentationTypeDto>> Handle(RepresentationTypeReadsCommandReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.RepresentationType> list = await _representationTypeRep.GetAll();
        return _mapper.Map<List<RepresentationTypeDto>>(list);
    }
}