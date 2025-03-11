using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.SectionDetail.Requests.Commands;

namespace Shop.Application.Features.SectionDetail.Handlers.Commands
{
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
}
