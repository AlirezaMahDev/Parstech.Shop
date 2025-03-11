using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.State;
using Shop.Application.Features.State.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.State.Handlers.Commands
{
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
}
