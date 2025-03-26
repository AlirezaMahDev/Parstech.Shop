using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.UserStore.Handlers.Queries;

public class UserStoreOfUserReadQueryHandler : IRequestHandler<UserStoreOfUserReadQueryReq, UserStoreDto>
{
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreOfUserReadQueryHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<UserStoreDto> Handle(UserStoreOfUserReadQueryReq request, CancellationToken cancellationToken)
    {
        var user = await _userStoreRep.GetStoreOfUser(request.userId);
        return _mapper.Map<UserStoreDto>(user);
    }
}