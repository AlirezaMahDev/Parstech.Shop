using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Commands;

public class CateguryCreateCommandHandler : IRequestHandler<CateguryCreateCommandReq, CateguryDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguryCreateCommandHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<CateguryDto> Handle(CateguryCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Categury? categury = _mapper.Map<Shared.Models.Categury>(request.CateguryDto);
        categury.Image = "05.png";
        Shared.Models.Categury categuryResult = await _categuryRep.AddAsync(categury);

        return _mapper.Map<CateguryDto>(categuryResult);
    }
}