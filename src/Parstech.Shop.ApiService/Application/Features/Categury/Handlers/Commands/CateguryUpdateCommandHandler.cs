using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

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
        Shared.Models.Categury? categury = _mapper.Map<Shared.Models.Categury>(request.CateguryDto);
        Shared.Models.Categury categuryResult = await _categuryRep.UpdateAsync(categury);
        return _mapper.Map<CateguryDto>(categuryResult);
    }
}