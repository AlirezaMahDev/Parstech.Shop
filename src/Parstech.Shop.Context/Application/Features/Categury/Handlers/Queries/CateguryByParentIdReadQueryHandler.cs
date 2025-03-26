using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

public class CateguryByParentIdReadQueryHandler : IRequestHandler<CateguryByParentIdReadQueryReq, List<CateguryDto>>
{
    private ICateguryRepository _categuryRep;
    private IMapper _mapper;

    public CateguryByParentIdReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<List<CateguryDto>> Handle(CateguryByParentIdReadQueryReq request, CancellationToken cancellationToken)
    {
        var list = await _categuryRep.GetCateguryByParentId(request.ParentId,null);
        return _mapper.Map<List<CateguryDto>>(list);
    }
}