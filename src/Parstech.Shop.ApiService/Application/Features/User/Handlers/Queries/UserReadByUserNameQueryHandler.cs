using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class UserReadByUserNameQueryHandler : IRequestHandler<UserReadByUserNameQueryReq, UserDto>
{
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;

    public UserReadByUserNameQueryHandler(IUserRepository userRep, IMapper mapper)
    {
        _userRep = userRep;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(UserReadByUserNameQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        return _mapper.Map<UserDto>(user);
    }
}