using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

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
        var cat =await _categuryRep.GetAsync(request.id);
        return _mapper.Map<CateguryDto>(cat);

    }
}