using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class CateguryReadByIdQueryHandler : IRequestHandler<CateguryReadByIdQueryReq, CateguryDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguryReadByIdQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<CateguryDto> Handle(CateguryReadByIdQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Categury? cat = await _categuryRep.GetAsync(request.id);
        return _mapper.Map<CateguryDto>(cat);
    }
}