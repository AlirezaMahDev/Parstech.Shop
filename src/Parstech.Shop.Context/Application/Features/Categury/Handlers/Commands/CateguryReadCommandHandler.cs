using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Commands;

public class CateguryOneReadQueryHandler : IRequestHandler<CateguryOneReadCommandReq, CateguryDto>
{
    private ICateguryRepository _categuryRep;
    private IMapper _mapper;

    public CateguryOneReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<CateguryDto> Handle(CateguryOneReadCommandReq request, CancellationToken cancellationToken)
    {
        var item = await _categuryRep.GetAsync(request.categuryId);
        return _mapper.Map<CateguryDto>(item);
    }
}
    
    
    
    
public class CateguryReadCommandHandler : IRequestHandler<CateguryReadCommandReq, List<CateguryDto>>
{
    private ICateguryRepository _categuryRep;
    private IMapper _mapper;

    public CateguryReadCommandHandler(ICateguryRepository categuryRep, IMapper mapper)
    {
        _categuryRep = categuryRep;
        _mapper = mapper;
    }
    public async Task<List<CateguryDto>> Handle(CateguryReadCommandReq request, CancellationToken cancellationToken)
    {
        var list = await _categuryRep.GetAll();

        if (request.filter != null)
        {
            list=list.Where(u=>u.GroupTitle.Contains(request.filter)).ToList();
        }
        return _mapper.Map<List<CateguryDto>>(list);
    }
}