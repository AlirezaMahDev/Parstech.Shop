using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.SectionDetail.Handlers.Commands;

public class SectionDetailReadCommandHandler : IRequestHandler<SectionDetailReadCommandReq, SectionDetailDto>
{
    private ISectionDetailRepository _sectionDetailRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public SectionDetailReadCommandHandler(ISectionDetailRepository sectionDetailRep,
        IMapper mapper,
        IMediator madiiator)
    {
        _sectionDetailRep = sectionDetailRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<SectionDetailDto> Handle(SectionDetailReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.SectionDetail? section = await _sectionDetailRep.GetAsync(request.id);
        return _mapper.Map<SectionDetailDto>(section);
    }
}