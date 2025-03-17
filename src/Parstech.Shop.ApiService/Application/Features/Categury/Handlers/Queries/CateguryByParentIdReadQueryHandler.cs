using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class CateguryByParentIdReadQueryHandler : IRequestHandler<CateguryByParentIdReadQueryReq, List<CateguryDto>>
{
    private ICateguryRepository _categuryRep;
    private IMapper _mapper;

    public CateguryByParentIdReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<List<CateguryDto>> Handle(CateguryByParentIdReadQueryReq request,
        CancellationToken cancellationToken)
    {
        List<Domain.Models.Categury>? list = await _categuryRep.GetCateguryByParentId(request.ParentId, null);
        return _mapper.Map<List<CateguryDto>>(list);
    }
}