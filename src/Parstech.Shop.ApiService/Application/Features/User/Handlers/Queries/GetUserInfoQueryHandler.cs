using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQueryReq, UserInfoDto>
    {
        private readonly IUserRepository _userRep;
        private readonly IMapper _mapper;
        private readonly IUserBillingRepository _userBillingRep;

        public GetUserInfoQueryHandler(IUserRepository userRep,
            IMapper mapper,
            IUserBillingRepository userBillingRep)
        {
            _userRep = userRep;
            _mapper = mapper;
            _userBillingRep = userBillingRep;
        }
        public async Task<UserInfoDto> Handle(GetUserInfoQueryReq request, CancellationToken cancellationToken)
        {
            var userInfo = new UserInfoDto();
            if (request.userName != null)
            {
                var user = await _userRep.GetUserByUserName(request.userName);
                var userBilling = await _userBillingRep.GetUserBillingByUserId(user.Id);
                var userRoles = await _userRep.GetUserRoles(user.UserId);

                userInfo.FullName = $"{userBilling.FirstName} {userBilling.LastName}";
                userInfo.Role = userRoles.FirstOrDefault().PersianRoleName;
                userInfo.LastLoginShamsi = $"{user.LastLoginDate.ToShamsi()}";
            }
            else
            {
                userInfo.FullName = "-";
            }
            userInfo.Position = request.position;
            return userInfo;
        }
    }
}
