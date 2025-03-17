using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class CateguryParentsReadQueryHandler : IRequestHandler<CateguryParentsReadQueryReq, List<CateguryDto>>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguryParentsReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<List<CateguryDto>> Handle(CateguryParentsReadQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Domain.Models.Categury>? list = await _categuryRep.GetAllParentCategury();
        return _mapper.Map<List<CateguryDto>>(list);
    }
}