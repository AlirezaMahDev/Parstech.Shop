using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Commands;

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
        Domain.Models.UserStore? userStore = _mapper.Map<Domain.Models.UserStore>(request.userStoreDto);

        Domain.Models.UserStore? userResult = await _userStoreRep.UpdateAsync(userStore);
        return _mapper.Map<UserStoreDto>(userResult);
    }
}