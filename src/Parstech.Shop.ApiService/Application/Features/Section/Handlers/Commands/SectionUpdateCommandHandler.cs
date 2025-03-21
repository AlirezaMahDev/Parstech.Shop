﻿using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Commands;

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
        Shared.Models.Section? section = await _sectionRep.GetAsync(request.SectionDto.Id);
        _mapper.Map(request.SectionDto, section);
        await _sectionRep.UpdateAsync(section);
        var result = await _madiiator.Send(new SectionReadCommandReq(section.Id));
        return result;
    }
}