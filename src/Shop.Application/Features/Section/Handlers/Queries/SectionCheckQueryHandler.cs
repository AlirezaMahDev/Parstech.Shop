using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Section.Requests.Queries;

namespace Shop.Application.Features.Section.Handlers.Queries
{
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
}
