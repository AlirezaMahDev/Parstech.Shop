using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Security.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.User.Handlers.Queries
{
    public class LoginByActiveCodeQueryHandler : IRequestHandler<LoginByActiveCodeQueryReq, ResponseDto>
    {

        private readonly IUserRepository _userRep;
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;


        public LoginByActiveCodeQueryHandler(IUserRepository userRep, IMediator mediator,
            SignInManager<IdentityUser> signInManager)
        {
            _userRep = userRep;
            _mediator = mediator;
            _signInManager = signInManager;
        }
        public async Task<ResponseDto> Handle(LoginByActiveCodeQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            var item = await _userRep.GetUserByUserName(request.Mobile);
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

            Random random = new Random();
            var activeCode = random.Next(10000000, 99999999);
            item.ActiveCode = activeCode.ToString();
            await _userRep.UpdateAsync(item);


            var iuser = await _userRep.GetIUserByUserName(request.Mobile);
            await _signInManager.SignInAsync(iuser,true);

            Response.IsSuccessed = true;
            Response.Message = "با موفقیت وارد شدید ، در حال انتقال به پنل کاربری";
            Response.Object2 = await _mediator.Send(new DataProtectQueryReq(request.Mobile, "protect"));
            return Response;
        }
    }
}
