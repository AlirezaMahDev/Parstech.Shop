using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.IRole;
using Shop.Application.Features.IRole.Requests.Commands;
using Shop.Domain.Models;

namespace Shop.Application.Features.IRole.Handlers.Commands
{
    public class IRoleReadCommandHandler : IRequestHandler<IRoleReadCommandReq, IRoleDto>
    {
        private IRoleRepository _roleRep;
        private readonly IMapper _mapper;

        public IRoleReadCommandHandler(IRoleRepository roleRep, IMapper mapper)
        {
            _roleRep = roleRep;
            _mapper = mapper;
        }
        public async Task<IRoleDto> Handle(IRoleReadCommandReq request, CancellationToken cancellationToken)
        {
           var role=await _roleRep.GetIdentityRole(request.id);
           return _mapper.Map<IRoleDto>(role);
        }
    }
    
    public class IRoleReadAllCommandHandler : IRequestHandler<IRoleReadAllCommandReq, List<IRoleDto>>
    {
        private IRoleRepository _roleRep;
        private readonly IMapper _mapper;

        public IRoleReadAllCommandHandler(IRoleRepository roleRep, IMapper mapper)
        {
            _roleRep = roleRep;
            _mapper = mapper;
        }
        public async Task<List<IRoleDto>> Handle(IRoleReadAllCommandReq request, CancellationToken cancellationToken)
        {
            var roles = await _roleRep.GetAll();
            return _mapper.Map<List<IRoleDto>>(roles);
        }
    }
}
