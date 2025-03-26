using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Commands;

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
        var categury = _mapper.Map<Domain.Models.Categury>(request.CateguryDto);
        categury.Image = "05.png";
        var categuryResult = await _categuryRep.AddAsync(categury);

        return _mapper.Map<CateguryDto>(categuryResult);
    }
}