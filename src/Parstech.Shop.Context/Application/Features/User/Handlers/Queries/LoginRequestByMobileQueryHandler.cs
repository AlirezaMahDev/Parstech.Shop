using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

public class LoginRequestByMobileQueryHandler : IRequestHandler<LoginRequestByMobileQueryReq, ResponseDto>
{
    private readonly IUserRepository _userRep;
    public LoginRequestByMobileQueryHandler(IUserRepository userRep)
    {

        _userRep=userRep;
    }
    public async Task<ResponseDto> Handle(LoginRequestByMobileQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response=new();
        var item =await _userRep.GetUserByUserName(request.Mobile);
        if (item == null)
        {
            Response.IsSuccessed = false;
            Response.Message = "کاربری با شماره همراه وارد شده یافت نشد";
            return Response;
        }

        Random random=new();
        var activeCode= random.Next(1000, 9999);
        item.ActiveCode = activeCode.ToString();
        await _userRep.UpdateAsync(item);
        var userName =long.Parse(item.UserName);
        Response = Sms.Sms.SendCode(78876, userName, item.ActiveCode);

        return Response;
    }
}