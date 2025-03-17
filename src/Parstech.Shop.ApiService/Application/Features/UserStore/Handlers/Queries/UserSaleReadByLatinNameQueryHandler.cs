using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserStore.Handlers.Queries;

public class UserSaleReadByLatinNameQueryHandler : IRequestHandler<UserSaleReadByLatinNameQueryReq, UserStoreDto>
{
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IMapper _mapper;

    public UserSaleReadByLatinNameQueryHandler(IUserStoreRepository userStoreRep,
        IMapper mapper)
    {
        _userStoreRep = userStoreRep;
        _mapper = mapper;
    }

    public async Task<UserStoreDto> Handle(UserSaleReadByLatinNameQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.UserStore item = await _userStoreRep.GetStoreByLatinName(request.latinName);
        return _mapper.Map<UserStoreDto>(item);
    }
}