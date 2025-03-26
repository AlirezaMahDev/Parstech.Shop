using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Section;
using Parstech.Shop.Context.Application.Features.Section.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Section.Handlers.Commands;

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
        var section = _mapper.Map<Domain.Models.Section>(request.SectionDto);
        await _sectionRep.AddAsync(section);
        var result = await _madiiator.Send(new SectionReadCommandReq(section.Id));
        return result;
    }
}