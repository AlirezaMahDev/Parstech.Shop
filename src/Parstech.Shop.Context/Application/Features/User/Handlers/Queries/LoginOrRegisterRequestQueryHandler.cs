using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

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
        var activeCode = random.Next(1000, 9999);
        var item = await _userRep.GetUserByUserName(request.Mobile);
        if (item == null)
        {
                
            long mobile=long.Parse(request.Mobile);
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
            var userName = long.Parse(item.UserName);
            Response = Sms.Sms.SendCode(78876, userName, item.ActiveCode);
            StatusResponse.Message = "کاربر قدیمی";
            StatusResponse.IsSuccessed = true;
                
            Response.Object2 = StatusResponse;
        }

            
           

        return Response;
    }
}