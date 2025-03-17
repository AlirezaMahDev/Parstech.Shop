using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class LoginOrRegisterRequestQueryHandler : IRequestHandler<LoginOrRegisterRequestQueryReq, ResponseDto>
{
    private readonly IUserRepository _userRep;

    public LoginOrRegisterRequestQueryHandler(IUserRepository userRep)
    {
        _userRep = userRep;
    }


    public async Task<ResponseDto> Handle(LoginOrRegisterRequestQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        ResponseDto StatusResponse = new();
        Random random = new();
        int activeCode = random.Next(1000, 9999);
        Domain.Models.User? item = await _userRep.GetUserByUserName(request.Mobile);
        if (item == null)
        {
            long mobile = long.Parse(request.Mobile);
            Response = Sms.Sms.SendCode(78876, mobile, activeCode.ToString());
            StatusResponse.Message = "کاربر جدید";
            StatusResponse.IsSuccessed = false;
            StatusResponse.Object = activeCode.ToString();
            Response.Object2 = StatusResponse;
        }
        else
        {
            item.ActiveCode = activeCode.ToString();
            await _userRep.UpdateAsync(item);
            long userName = long.Parse(item.UserName);
            Response = Sms.Sms.SendCode(78876, userName, item.ActiveCode);
            StatusResponse.Message = "کاربر قدیمی";
            StatusResponse.IsSuccessed = true;

            Response.Object2 = StatusResponse;
        }


        return Response;
    }
}