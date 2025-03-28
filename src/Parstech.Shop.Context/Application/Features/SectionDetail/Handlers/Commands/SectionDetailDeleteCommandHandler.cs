﻿using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.SectionDetail.Handlers.Commands;

public class SectionDetailDeleteCommandHandler : IRequestHandler<SectionDetailDeleteCommandReq, Unit>
{

    private readonly ISectionDetailRepository _sectionDetailRep;

    public SectionDetailDeleteCommandHandler(ISectionDetailRepository sectionDetailRep)
    {
        _sectionDetailRep = sectionDetailRep;
    }
    public async Task<Unit> Handle(SectionDetailDeleteCommandReq request, CancellationToken cancellationToken)
    {
        var sectionDetail =await _sectionDetailRep.GetAsync(request.id);
        await _sectionDetailRep.DeleteAsync(sectionDetail);
        return Unit.Value;
    }
}