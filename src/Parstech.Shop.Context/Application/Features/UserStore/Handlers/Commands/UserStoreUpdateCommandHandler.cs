using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserStore.Handlers.Commands;

public class UserStoreUpdateCommandHandler : IRequestHandler<UserStoreUpdateCommandReq, UserStoreDto>
{
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreUpdateCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }
    public async Task<UserStoreDto> Handle(UserStoreUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var userStore = _mapper.Map<Domain.Models.UserStore>(request.userStoreDto);

        var userResult = await _userStoreRep.UpdateAsync(userStore);
        return _mapper.Map<UserStoreDto>(userResult);
    }
}