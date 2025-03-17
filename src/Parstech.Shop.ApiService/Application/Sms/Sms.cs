using Parstech.Shop.Shared.DTOs;

using SmsIrRestful;

namespace Parstech.Shop.ApiService.Application.Sms;

public static class Sms
{
    #region Sms

    public static ResponseDto SendCode(int TemplateCode, long Mobile, string ActiveCode)
    {
        ResponseDto Response = new();
        try
        {
            string? token = new Token().GetToken("a1713ec9d1d0643587033ec9", "ti7t7gkjgkkhvcrtdjgkjh");


            UltraFastSend ultraFastSend = new()
            {
                Mobile = Mobile,
                TemplateId = TemplateCode,
                ParameterArray = new List<UltraFastParameters>()
                {
                    new() { Parameter = "ActiveCode", ParameterValue = ActiveCode }
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