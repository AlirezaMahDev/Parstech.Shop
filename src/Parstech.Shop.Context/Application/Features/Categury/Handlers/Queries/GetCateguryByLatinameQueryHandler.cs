using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

public class GetCateguryByLatinameQueryHandler : IRequestHandler<GetCateguryByLatinameQueryReq, CateguryDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;
    public GetCateguryByLatinameQueryHandler(ICateguryRepository categuryRep,IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<CateguryDto> Handle(GetCateguryByLatinameQueryReq request, CancellationToken cancellationToken)
    {
        var item =await _categuryRep.GetCateguryByLatinName(request.latinName);
        return _mapper.Map<CateguryDto>(item);
    }
}