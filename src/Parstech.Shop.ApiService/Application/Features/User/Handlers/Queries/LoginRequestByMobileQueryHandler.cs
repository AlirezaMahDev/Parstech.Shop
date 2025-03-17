using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class LoginRequestByMobileQueryHandler : IRequestHandler<LoginRequestByMobileQueryReq, ResponseDto>
{
    private readonly IUserRepository _userRep;

    public LoginRequestByMobileQueryHandler(IUserRepository userRep)
    {
        _userRep = userRep;
    }

    public async Task<ResponseDto> Handle(LoginRequestByMobileQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        Shared.Models.User? item = await _userRep.GetUserByUserName(request.Mobile);
        if (item == null)
        {
            Response.IsSuccessed = false;
            Response.Message = "کاربری با شماره همراه وارد شده یافت نشد";
            return Response;
        }

        Random random = new();
        int activeCode = random.Next(1000, 9999);
        item.ActiveCode = activeCode.ToString();
        await _userRep.UpdateAsync(item);
        long userName = long.Parse(item.UserName);
        Response = Sms.Sms.SendCode(78876, userName, item.ActiveCode);

        return Response;
    }
}