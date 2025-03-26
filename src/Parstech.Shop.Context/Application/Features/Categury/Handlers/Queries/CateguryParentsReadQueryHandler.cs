using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

public class CateguryParentsReadQueryHandler : IRequestHandler<CateguryParentsReadQueryReq, List<CateguryDto>>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public CateguryParentsReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<List<CateguryDto>> Handle(CateguryParentsReadQueryReq request, CancellationToken cancellationToken)
    {
        var list =await _categuryRep.GetAllParentCategury();
        return _mapper.Map<List<CateguryDto>>(list);
    }
}