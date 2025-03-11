using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Application.Features.UserStore.Handlers.Queries
{
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
            var list =await _userStoreRep.GetAll();
            List<UserStoreDto> Result = new List<UserStoreDto>();
            foreach (var userStore in list)
            {
                var user =await _userRep.GetAsync(userStore.UserId);
                var usDto = _mapper.Map<UserStoreDto>(userStore);
                usDto.UserName = user.UserName;
                Result.Add(usDto);
            }

            return Result;
        }
    }
}
