using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.FormCredit;
using Shop.Application.Features.FormCredit.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.FormCredit.Handlers.Commands
{
    public class ReadFormCreditCommandHandler : IRequestHandler<ReadFormCreditCommandReq, FormCreditDto>
    {
        private readonly IFormCreditRepository _formCreditRep;
        private readonly IMapper _mapper;
        public ReadFormCreditCommandHandler(IFormCreditRepository formCreditRep, IMapper mapper)
        {
            _formCreditRep = formCreditRep;
            _mapper = mapper;
        }
        public async Task<FormCreditDto> Handle(ReadFormCreditCommandReq request, CancellationToken cancellationToken)
        {
            var item =await _formCreditRep.GetAsync(request.Id);
            return _mapper.Map<FormCreditDto>(item);    
        }
    }

    
}
