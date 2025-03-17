using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class GetCateguryByLatinameQueryHandler : IRequestHandler<GetCateguryByLatinameQueryReq, CateguryDto>
{
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public GetCateguryByLatinameQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<CateguryDto> Handle(GetCateguryByLatinameQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Categury? item = await _categuryRep.GetCateguryByLatinName(request.latinName);
        return _mapper.Map<CateguryDto>(item);
    }
}