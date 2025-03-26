using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.UserStore.Handlers.Queries;

public class UserSaleReadByLatinNameQueryHandler : IRequestHandler<UserSaleReadByLatinNameQueryReq, UserStoreDto>
{
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IMapper _mapper;
    public UserSaleReadByLatinNameQueryHandler(IUserStoreRepository userStoreRep,
        IMapper mapper)
    {
        _userStoreRep= userStoreRep;
        _mapper= mapper;
    }
    public async Task<UserStoreDto> Handle(UserSaleReadByLatinNameQueryReq request, CancellationToken cancellationToken)
    {
        var item =await _userStoreRep.GetStoreByLatinName(request.latinName);
        return _mapper.Map<UserStoreDto>(item);
    }
}