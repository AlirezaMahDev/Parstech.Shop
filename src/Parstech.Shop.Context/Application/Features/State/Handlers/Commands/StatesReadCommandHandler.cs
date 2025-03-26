using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.State;
using Parstech.Shop.Context.Application.Features.State.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.State.Handlers.Commands;

public class StatesReadCommandHandler : IRequestHandler<StatesReadsCommandReq, List<SteteDto>>
{
    private readonly IStateRepository _stateRep;
    private readonly IMapper _mapper;
    public StatesReadCommandHandler(IStateRepository stateRep, IMapper mapper)
    {
        _stateRep = stateRep;
        _mapper = mapper;
    }
    public async Task<List<SteteDto>> Handle(StatesReadsCommandReq request, CancellationToken cancellationToken)
    {
        var result = await _stateRep.GetAll();
        return _mapper.Map<List<SteteDto>>(result); 
    }
}