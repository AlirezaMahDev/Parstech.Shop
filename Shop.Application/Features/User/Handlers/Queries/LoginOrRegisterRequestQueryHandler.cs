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
    public class LoginOrRegisterRequestQueryHandler : IRequestHandler<LoginOrRegisterRequestQueryReq, ResponseDto>
    {
        private readonly IUserRepository _userRep;
        public LoginOrRegisterRequestQueryHandler(IUserRepository userRep)
        {

            _userRep = userRep;
        }
        
            
        
        public async Task<ResponseDto> Handle(LoginOrRegisterRequestQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            ResponseDto StatusResponse = new ResponseDto();
            Random random = new Random();
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
}
