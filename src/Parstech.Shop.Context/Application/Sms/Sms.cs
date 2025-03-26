using Parstech.Shop.Context.Application.DTOs.Response;

using SmsIrRestful;

namespace Parstech.Shop.Context.Application.Sms;

public static class Sms
{
    #region Sms
    public static ResponseDto SendCode(int TemplateCode, long Mobile, string ActiveCode)
    {
        var Response = new ResponseDto();
        try
        {
            var token = new Token().GetToken("a1713ec9d1d0643587033ec9", "ti7t7gkjgkkhvcrtdjgkjh");


            var ultraFastSend = new UltraFastSend()
            {
                Mobile = Mobile,
                TemplateId = TemplateCode,
                ParameterArray = new List<UltraFastParameters>()
                {
                    new()
                    {
                        Parameter = "ActiveCode" , ParameterValue =ActiveCode
                    },

                }.ToArray()

            };

            UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);

            if (ultraFastSendRespone.IsSuccessful)
            {
                Response.IsSuccessed = true;
                Response.Message = "کد تائید برای شما ارسال گردید";

            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "در حال حاضر ارسال پیام به شما امکان پذیر نیست";

            }
            return Response;
        }
        catch (Exception ex)
        {
            Response.IsSuccessed = false;
            Response.Message = "خطا در سامانه پیامکی";


            return Response;
        }


    }


    #endregion
}