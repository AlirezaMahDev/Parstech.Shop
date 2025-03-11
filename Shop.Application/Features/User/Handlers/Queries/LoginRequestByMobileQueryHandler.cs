using MediatR;
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
    public class LoginRequestByMobileQueryHandler : IRequestHandler<LoginRequestByMobileQueryReq, ResponseDto>
    {
        private readonly IUserRepository _userRep;
        public LoginRequestByMobileQueryHandler(IUserRepository userRep)
        {

            _userRep=userRep;
        }
        public async Task<ResponseDto> Handle(LoginRequestByMobileQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response=new ResponseDto();
            var item =await _userRep.GetUserByUserName(request.Mobile);
            if (item == null)
            {
                Response.IsSuccessed = false;
                Response.Message = "کاربری با شماره همراه وارد شده یافت نشد";
                return Response;
            }

            Random random=new Random();
            var activeCode= random.Next(1000, 9999);
            item.ActiveCode = activeCode.ToString();
            await _userRep.UpdateAsync(item);
            var userName =long.Parse(item.UserName);
            Response = Sms.Sms.SendCode(78876, userName, item.ActiveCode);

            return Response;
        }
    }
}
