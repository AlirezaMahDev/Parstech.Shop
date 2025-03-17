using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;

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
        Domain.Models.FormCredit? item = await _formCreditRep.GetAsync(request.Id);
        return _mapper.Map<FormCreditDto>(item);
    }
}