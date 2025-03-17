using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Queries;

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