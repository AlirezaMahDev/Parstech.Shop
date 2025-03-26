using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Representation.Handlers.Commands;

public class RepresentationCreateCommandHandler : IRequestHandler<RepresentationCreateCommandReq, RepresentationDto>
{
    private IRepresentationRepository _represntationRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public RepresentationCreateCommandHandler(IRepresentationRepository represntationRep, IMapper mapper, IMediator madiiator)
    {
        _represntationRep = represntationRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }
    public async Task<RepresentationDto> Handle(RepresentationCreateCommandReq request, CancellationToken cancellationToken)
    {
        var rep = _mapper.Map<Domain.Models.Representation>(request.RepresentationDto);
            
        var userResult=await _represntationRep.AddAsync(rep);
        return _mapper.Map<RepresentationDto>(userResult);
    }
}