using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Commands;

public class CateguryUpdateCommandHandler : IRequestHandler<CateguryUpdateCommandReq, CateguryDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguryUpdateCommandHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<CateguryDto> Handle(CateguryUpdateCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Categury? categury = _mapper.Map<Domain.Models.Categury>(request.CateguryDto);
        Domain.Models.Categury? categuryResult = await _categuryRep.UpdateAsync(categury);
        return _mapper.Map<CateguryDto>(categuryResult);
    }
}