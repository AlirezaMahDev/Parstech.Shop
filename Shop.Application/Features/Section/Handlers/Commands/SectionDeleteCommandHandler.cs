using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Section.Requests.Commands;

namespace Shop.Application.Features.Section.Handlers.Commands
{
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
}
