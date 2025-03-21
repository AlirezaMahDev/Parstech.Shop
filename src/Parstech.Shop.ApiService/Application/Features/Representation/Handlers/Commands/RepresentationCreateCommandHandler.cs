﻿using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

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
        Shared.Models.Representation? rep = _mapper.Map<Shared.Models.Representation>(request.RepresentationDto);

        Shared.Models.Representation userResult = await _represntationRep.AddAsync(rep);
        return _mapper.Map<RepresentationDto>(userResult);
    }
}