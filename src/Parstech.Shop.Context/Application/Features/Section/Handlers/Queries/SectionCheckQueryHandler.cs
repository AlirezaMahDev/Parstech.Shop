using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Section.Handlers.Queries;

internal class SectionCheckQueryHandler : IRequestHandler<SectionCheckQueryReq, bool>
{

    private readonly ISectionRepository _sectionRep;

    public SectionCheckQueryHandler(ISectionRepository sectionRep)
    {
        _sectionRep = sectionRep;
    }

    public async Task<bool> Handle(SectionCheckQueryReq request, CancellationToken cancellationToken)
    {
        return await _sectionRep.CheckSectionDetailExist(request.id);
    }
}