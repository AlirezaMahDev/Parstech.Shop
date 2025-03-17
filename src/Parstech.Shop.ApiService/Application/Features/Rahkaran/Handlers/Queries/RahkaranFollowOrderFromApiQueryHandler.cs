using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

using RestSharp;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Queries;

public class RahkaranFollowOrderFromApiQueryHandler : IRequestHandler<RahkaranFollowOrderFromApiQueryReq, ResponseDto>
{
    private readonly RestClient client;
    private readonly RestRequest Request;
    private readonly IMediator _mediator;

    private readonly RestRequest FollowFactor;

    public RahkaranFollowOrderFromApiQueryHandler(IMediator mediator)
    {
        client = new("https://marketing.parstech.co");
        Request = new("/api/Rahkaran/", Method.Post);
        FollowFactor = new("/api/Follow/", Method.Post);
        _mediator = mediator;
    }

    public async Task<ResponseDto> Handle(RahkaranFollowOrderFromApiQueryReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        QuotationFollowUpResult result = new();
        QuotationFollowUpRequest data = new();

        RahkaranAllDto RahkaranAll = await _mediator.Send(new RahakaranAllQueryReq(request.orderId));
        data.IDQ = long.Parse(RahkaranAll.order.RahkaranPishNumber);

        string JsonData = JsonConvert.SerializeObject(data);
        FollowFactor.AddJsonBody(JsonData);

        //var getTokenResult= client.Post(getTokenRequest);
        try
        {
            QuotationFollowUpResult? finalResult = client.Post<QuotationFollowUpResult>(FollowFactor);
            if (finalResult.OutNumber != "")
            {
                RahkaranAll.order.RahakaranFactorSerial = finalResult.OutNumber;
                RahkaranAll.order.RahakaranFactorNumber = finalResult.OutInvoice.ToString();
                await _mediator.Send(new RahkaranOrderUpdateCommandReq(RahkaranAll.order));
                response.IsSuccessed = true;
                response.Message = "استعلام موفق(فاکتور در سامانه راهکاران صادر شده است)";

                return response;
            }

            response.IsSuccessed = false;
            response.Message = "خطای وب سرویس (فاکتوری از سامانه راهکاران دریافت نشد)";

            return response;
        }
        catch (Exception e)
        {
            response.IsSuccessed = false;
            response.Message = "خطای استعلام (فاکتوری از سامانه راهکاران دریافت نشد)";

            return response;
        }
    }
}