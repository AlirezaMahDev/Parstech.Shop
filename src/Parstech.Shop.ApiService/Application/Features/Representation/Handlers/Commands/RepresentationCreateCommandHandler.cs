using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Representation.Handlers.Commands;

public class RepresentationCreateCommandHandler : IRequestHandler<RepresentationCreateCommandReq, RepresentationDto>
{
    private IRepresentationRepository _represntationRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public RepresentationCreateCommandHandler(IRepresentationRepository represntationRep,
        IMapper mapper,
        IMediator madiiator)
    {
        _represntationRep = represntationRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<RepresentationDto> Handle(RepresentationCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        Domain.Models.Representation? rep = _mapper.Map<Domain.Models.Representation>(request.RepresentationDto);

        Domain.Models.Representation? userResult = await _represntationRep.AddAsync(rep);
        return _mapper.Map<RepresentationDto>(userResult);
    }
}