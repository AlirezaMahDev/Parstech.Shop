using Newtonsoft.Json;

using Parstech.Shop.Context.Application.Dargah.ZarrinPal.Models;
using Parstech.Shop.Context.Application.Dargah.ZarrinPal.Models.withExtra;

namespace Parstech.Shop.Context.Application.Dargah.ZarrinPal;

public class ZarinPal
{




    private static ZarinPal _ZarinPal;
    private HttpCore _HttpCore;
    private Boolean IsSandBox;
    public PaymentRequest PaymentRequest { get; private set; }


    private ZarinPal()
    {
        this._HttpCore = new();
    }



    public static ZarinPal Get()
    {
        if (_ZarinPal == null)
        {
            _ZarinPal = new();
        }


        return _ZarinPal;
    }


    public void EnableSandboxMode()
    {
        this.IsSandBox = true;
    }

    public void DisableSandboxMode()
    {
        this.IsSandBox = false;
    }


    public PaymentResponse InvokePaymentRequest(PaymentRequest PaymentRequest)
    {
        URLs url = new(this.IsSandBox);
        _HttpCore.URL = url.GetPaymentRequestURL();
        _HttpCore.Method = Method.POST;
        _HttpCore.Raw = PaymentRequest;
        this.PaymentRequest = PaymentRequest;
        String response = _HttpCore.Get();


        PaymentResponse _Response = JsonConvert.DeserializeObject<PaymentResponse>(response);
        _Response.PaymentURL = url.GetPaymenGatewayURL(_Response.Authority);

        return _Response;
    }


    public VerificationResponse InvokePaymentVerification(PaymentVerification verificationRequest)
    {
        URLs url = new(this.IsSandBox);
        _HttpCore.URL = url.GetVerificationURL();
        _HttpCore.Method = Method.POST;
        _HttpCore.Raw = verificationRequest;


        String response =  _HttpCore.Get();

        VerificationResponse verification = JsonConvert.DeserializeObject<VerificationResponse>(response);

        return verification;

    }


    public PaymentResponse InvokePaymentRequestWithExtra(PaymentRequestWithExtra paymentRequestWithExtra){
        URLs url = new(this.IsSandBox,true);
        _HttpCore.URL = url.GetPaymentRequestURL();
        _HttpCore.Method = Method.POST;
        _HttpCore.Raw = PaymentRequest;
        this.PaymentRequest = PaymentRequest;
        String response = _HttpCore.Get();


        PaymentResponse _Response = JsonConvert.DeserializeObject<PaymentResponse>(response);
        _Response.PaymentURL = url.GetPaymenGatewayURL(_Response.Authority);

        return _Response;
    }




    public VerificationResponse InvokePaymentVerificationWithExtra(PaymentVerification verificationRequest)
    {
        URLs url = new(this.IsSandBox,true);
        _HttpCore.URL = url.GetVerificationURL();
        _HttpCore.Method = Method.POST;
        _HttpCore.Raw = verificationRequest;


        String response = _HttpCore.Get();

        VerificationResponse verification = JsonConvert.DeserializeObject<VerificationResponse>(response);

        return verification;

    }

}