using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

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
        var user =await _userRep.GetUserByUserName(request.userName);
        return _mapper.Map<UserDto>(user);
    }
}