using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.Features.FormCredit.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Handlers.Commands;

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