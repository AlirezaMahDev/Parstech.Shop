using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Security.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class LoginByActiveCodeQueryHandler : IRequestHandler<LoginByActiveCodeQueryReq, ResponseDto>
{
    private readonly IUserRepository _userRep;
    private readonly IMediator _mediator;
    private readonly SignInManager<IdentityUser> _signInManager;


    public LoginByActiveCodeQueryHandler(IUserRepository userRep,
        IMediator mediator,
        SignInManager<IdentityUser> signInManager)
    {
        _userRep = userRep;
        _mediator = mediator;
        _signInManager = signInManager;
    }

    public async Task<ResponseDto> Handle(LoginByActiveCodeQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        Shared.Models.User? item = await _userRep.GetUserByUserName(request.Mobile);
        if (item == null)
        {
            Response.IsSuccessed = false;
            Response.Message = "کاربری با شماره همراه وارد شده یافت نشد";
            return Response;
        }

        if (request.ActiveCode != item.ActiveCode)
        {
            Response.IsSuccessed = false;
            Response.Message = "کد تائید وارد شده نامعتبر است";
            return Response;
        }

        Random random = new();
        int activeCode = random.Next(10000000, 99999999);
        item.ActiveCode = activeCode.ToString();
        await _userRep.UpdateAsync(item);


        IdentityUser iuser = await _userRep.GetIUserByUserName(request.Mobile);
        await _signInManager.SignInAsync(iuser, true);

        Response.IsSuccessed = true;
        Response.Message = "با موفقیت وارد شدید ، در حال انتقال به پنل کاربری";
        Response.Object2 = await _mediator.Send(new DataProtectQueryReq(request.Mobile, "protect"));
        return Response;
    }
}