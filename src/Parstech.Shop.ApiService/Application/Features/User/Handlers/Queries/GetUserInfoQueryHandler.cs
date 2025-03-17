using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

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
        UserInfoDto userInfo = new();
        if (request.userName != null)
        {
            Shared.Models.User? user = await _userRep.GetUserByUserName(request.userName);
            Shared.Models.UserBilling? userBilling = await _userBillingRep.GetUserBillingByUserId(user.Id);
            List<IUserRoleDto> userRoles = await _userRep.GetUserRoles(user.UserId);

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