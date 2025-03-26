using MediatR;

using Microsoft.AspNetCore.Identity;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQueryReq, ResponseDto>
{
    private readonly IUserRepository _userRep;
    private readonly SignInManager<IdentityUser> _signInManager;


    public LogoutUserQueryHandler(IUserRepository userRep,
        SignInManager<IdentityUser> signInManager)
    {
        _userRep = userRep;
        _signInManager = signInManager;
    }
    public async Task<ResponseDto> Handle(LogoutUserQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto Response=new();
        await _signInManager.SignOutAsync();
        Response.IsSuccessed = true;
        Response.Message = "با موفقیت از حساب خود خارج شدید";
        return Response;
            
    }
}