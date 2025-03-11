using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Representation.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Shop.Application.Features.Representation.Handlers.Commands
{
    public class RepresentationCreateCommandHandler : IRequestHandler<RepresentationCreateCommandReq, RepresentationDto>
    {
        private IRepresentationRepository _represntationRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public RepresentationCreateCommandHandler(IRepresentationRepository represntationRep, IMapper mapper, IMediator madiiator)
        {
            _represntationRep = represntationRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }
        public async Task<RepresentationDto> Handle(RepresentationCreateCommandReq request, CancellationToken cancellationToken)
        {
            var rep = _mapper.Map<Domain.Models.Representation>(request.RepresentationDto);
            
            var userResult=await _represntationRep.AddAsync(rep);
            return _mapper.Map<RepresentationDto>(userResult);
        }
    }
}
