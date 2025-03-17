using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Commands;

public class UserReadCommandHandler : IRequestHandler<UserReadCommandReq, UserDto>
{
    private IUserRepository _userRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserReadCommandHandler(IUserRepository userRep, IMapper mapper, IMediator madiiator)
    {
        _userRep = userRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }


    public async Task<UserDto> Handle(UserReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.User? user = await _userRep.GetAsync(request.id);
        return _mapper.Map<UserDto>(user);
    }
}