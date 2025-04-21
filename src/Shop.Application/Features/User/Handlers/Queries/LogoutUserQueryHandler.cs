using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
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
            ResponseDto Response=new ResponseDto();
            await _signInManager.SignOutAsync();
            Response.IsSuccessed = true;
            Response.Message = "با موفقیت از حساب خود خارج شدید";
            return Response;
            
        }
    }
}
