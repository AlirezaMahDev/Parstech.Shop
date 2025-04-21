using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.UserStore.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.UserStore.Handlers.Queries
{
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
}
