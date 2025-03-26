using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Commands;

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
        var categury = _mapper.Map<Domain.Models.Categury>(request.CateguryDto);
        var categuryResult = await _categuryRep.UpdateAsync(categury);
        return _mapper.Map<CateguryDto>(categuryResult);
    }
}