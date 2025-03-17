using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Commands;

public class SectionReadCommandHandler : IRequestHandler<SectionReadCommandReq, SectionDto>
{
    private ISectionRepository _sectionRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public SectionReadCommandHandler(ISectionRepository sectionRep, IMapper mapper, IMediator madiiator)
    {
        _sectionRep = sectionRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<SectionDto> Handle(SectionReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Section? section = await _sectionRep.GetAsync(request.id);
        return _mapper.Map<SectionDto>(section);
    }
}