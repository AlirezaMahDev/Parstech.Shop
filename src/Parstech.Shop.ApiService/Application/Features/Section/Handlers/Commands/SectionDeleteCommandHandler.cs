using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Commands;

public class SectionDeleteCommandHandler : IRequestHandler<SectionDeleteCommandReq, Unit>
{
    private readonly ISectionRepository _sectionRep;

    public SectionDeleteCommandHandler(ISectionRepository sectionRep)
    {
        _sectionRep = sectionRep;
    }

    public async Task<Unit> Handle(SectionDeleteCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Section? section = await _sectionRep.GetAsync(request.id);
        await _sectionRep.DeleteAsync(section);
        return Unit.Value;
    }
}