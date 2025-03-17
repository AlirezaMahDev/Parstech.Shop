using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Handlers.Commands;

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
        Irole role = await _roleRep.GetIdentityRole(request.id);
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
        IReadOnlyList<Irole> roles = await _roleRep.GetAll();
        return _mapper.Map<List<IRoleDto>>(roles);
    }
}