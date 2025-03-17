using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.State.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.State.Handlers.Commands;

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
        IReadOnlyList<Domain.Models.State>? result = await _stateRep.GetAll();
        return _mapper.Map<List<SteteDto>>(result);
    }
}