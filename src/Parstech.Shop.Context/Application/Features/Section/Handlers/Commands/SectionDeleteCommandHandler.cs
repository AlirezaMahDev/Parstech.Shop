using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Section.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Section.Handlers.Commands;

public class SectionDeleteCommandHandler : IRequestHandler<SectionDeleteCommandReq, Unit>
{
    private readonly ISectionRepository _sectionRep;

    public SectionDeleteCommandHandler(ISectionRepository sectionRep)
    {
        _sectionRep = sectionRep;
    }

    public async Task<Unit> Handle(SectionDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var section =await _sectionRep.GetAsync(request.id);
        await _sectionRep.DeleteAsync(section);
        return Unit.Value;
    }
}