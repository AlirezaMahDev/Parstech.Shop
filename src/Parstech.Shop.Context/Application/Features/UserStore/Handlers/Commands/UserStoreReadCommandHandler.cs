using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.UserStore.Handlers.Commands;

public class UserStoreReadCommandHandler : IRequestHandler<UserStoreReadCommandReq, UserStoreDto>
{

    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreReadCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep   = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }


    public async Task<UserStoreDto> Handle(UserStoreReadCommandReq request, CancellationToken cancellationToken)
    {
        var item = await _userStoreRep.GetAsync(request.id);
        return _mapper.Map<UserStoreDto>(item);
    }
}
public class UserStoreReadsCommandHandler : IRequestHandler<UserStoreReadsCommandReq, List<UserStoreDto>>
{

    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreReadsCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }


    public async Task<List<UserStoreDto>> Handle(UserStoreReadsCommandReq request, CancellationToken cancellationToken)
    {
        var list = await _userStoreRep.GetAll();
        return _mapper.Map<List<UserStoreDto>>(list);
    }
}