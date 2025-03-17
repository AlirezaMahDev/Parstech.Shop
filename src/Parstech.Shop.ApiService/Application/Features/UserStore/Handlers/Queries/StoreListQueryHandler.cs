using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Queries;

public class StoreListQueryHandler : IRequestHandler<StoreListQueryReq, List<UserStoreDto>>
{
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IUserRepository _userRep;
    private readonly IMapper _mapper;

    public StoreListQueryHandler(IUserStoreRepository userStoreRep, IUserRepository userRep, IMapper mapper)
    {
        _userStoreRep = userStoreRep;
        _userRep = userRep;
        _mapper = mapper;
    }

    public async Task<List<UserStoreDto>> Handle(StoreListQueryReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.UserStore>? list = await _userStoreRep.GetAll();
        List<UserStoreDto> Result = new();
        foreach (Domain.Models.UserStore userStore in list)
        {
            Domain.Models.User? user = await _userRep.GetAsync(userStore.UserId);
            UserStoreDto? usDto = _mapper.Map<UserStoreDto>(userStore);
            usDto.UserName = user.UserName;
            Result.Add(usDto);
        }

        return Result;
    }
}