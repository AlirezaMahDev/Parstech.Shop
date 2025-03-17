using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Commands;

public class SectionCreateCommandHandler : IRequestHandler<SectionCreateCommandReq, SectionDto>
{
    private ISectionRepository _sectionRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public SectionCreateCommandHandler(ISectionRepository sectionRep, IMapper mapper, IMediator madiiator)
    {
        _sectionRep = sectionRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<SectionDto> Handle(SectionCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Section? section = _mapper.Map<Shared.Models.Section>(request.SectionDto);
        await _sectionRep.AddAsync(section);
        var result = await _madiiator.Send(new SectionReadCommandReq(section.Id));
        return result;
    }
}