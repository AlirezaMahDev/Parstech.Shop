using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Commands;

public class UserStoreCreateCommandHandler : IRequestHandler<UserStoreCreateCommandReq, UserStoreDto>
{
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreCreateCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<UserStoreDto> Handle(UserStoreCreateCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserStore? userStore = _mapper.Map<Shared.Models.UserStore>(request.userStoreDto);

        Shared.Models.UserStore userResult = await _userStoreRep.AddAsync(userStore);
        return _mapper.Map<UserStoreDto>(userResult);
    }
}