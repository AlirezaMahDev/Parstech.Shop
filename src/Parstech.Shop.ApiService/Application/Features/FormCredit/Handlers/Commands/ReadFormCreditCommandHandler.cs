using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Handlers.Commands;

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
        Shared.Models.FormCredit? item = await _formCreditRep.GetAsync(request.Id);
        return _mapper.Map<FormCreditDto>(item);
    }
}