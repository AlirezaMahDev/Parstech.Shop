using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Commands;

public class UserStoreReadCommandHandler : IRequestHandler<UserStoreReadCommandReq, UserStoreDto>
{
    private IUserStoreRepository _userStoreRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public UserStoreReadCommandHandler(IUserStoreRepository userStoreRep, IMapper mapper, IMediator madiiator)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }


    public async Task<UserStoreDto> Handle(UserStoreReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.UserStore? item = await _userStoreRep.GetAsync(request.id);
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
        IReadOnlyList<Domain.Models.UserStore>? list = await _userStoreRep.GetAll();
        return _mapper.Map<List<UserStoreDto>>(list);
    }
}