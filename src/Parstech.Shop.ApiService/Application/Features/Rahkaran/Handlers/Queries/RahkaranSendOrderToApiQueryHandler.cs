using MediatR;

using Newtonsoft.Json;

using Parstech.Shop.ApiService.Application.Calculator;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

using RestSharp;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Queries;

public class RahkaranSendOrderToApiQueryHandler : IRequestHandler<RahkaranSendOrderToApiQueryReq, ResponseDto>
{
    private readonly RestClient client;
    private readonly RestRequest Request;
    private readonly IMediator _mediator;

    private readonly RestRequest FollowFactor;

    public RahkaranSendOrderToApiQueryHandler(IMediator mediator)
    {
        client = new("https://marketing.parstech.co");
        //client = new RestClient("http://172.17.1.120:8081");
        Request = new("/api/Rahkaran/", Method.Post);
        FollowFactor = new("/api/Follow/", Method.Post);
        _mediator = mediator;
    }

    public async Task<ResponseDto> Handle(RahkaranSendOrderToApiQueryReq request, CancellationToken cancellationToken)
    {
        RahkaranAllDto RahkaranAll = await _mediator.Send(new RahakaranAllQueryReq(request.orderId));
        ResponseDto response = new();

        List<RakaranProductItem> items = new();


        foreach (RahkaranProductDto detail in RahkaranAll.products)
        {
            int? unitId = detail.RahkaranUnitId;
            long oneProductPrice = detail.Price.Value / detail.Count.Value;
            long Price = PersentCalculator.RahkaranCalvulatorByPrice(oneProductPrice);
            RakaranProductItem item = new()
            {
                ProductId = detail.RahkaranProductId.ToString().Trim(),
                UnitId = unitId.ToString().Trim(),
                Fee = Price.ToString().Trim(),
                Quantity = detail.Count.ToString().Trim()
            };
            items.Add(item);
        }


        RahkaranDto data = new();

        data = new()
        {
            system = "Parstech",
            orderCode = RahkaranAll.order.OrderCode,
            Currency = "1",
            Customer = RahkaranAll.customer.RahkaranUserId.ToString().Trim(),
            Payertype = "1",
            SalesTypeID = "1",
            SalesOfficeID = "21",
            Plant = "1",
            Inventory = "67",
            SalesAreaID = "4",
            Products = items
        };


        string JsonData = JsonConvert.SerializeObject(data);
        Request.AddJsonBody(JsonData);

        //var getTokenResult= client.Post(getTokenRequest);
        try
        {
            RahkaranResult? finalResult = client.Post<RahkaranResult>(Request);
            if (finalResult.QuotationId != "-")
            {
                response.IsSuccessed = true;
                response.Message = "سفارش با موفقیت در سامانه راهکاران ثبت گردید";

                response.Object2 = finalResult.Messages;
                RahkaranAll.order.RahkaranPishNumber = finalResult.QuotationId;
                await _mediator.Send(new RahkaranOrderUpdateCommandReq(RahkaranAll.order));
            }
            else
            {
                response.IsSuccessed = false;
                response.Message = "خطای وب سرویس (ارسال سفارش به سامانه راهکاران با شکست روبه رو شد)";

                response.Object2 = finalResult.Messages;
            }

            response.Object = JsonData;
            return response;
        }
        catch (Exception e)
        {
            response.IsSuccessed = false;
            response.Message = "خطای ارسال (ارسال سفارش به سامانه راهکاران با شکست روبه رو شد)";
            response.Object = JsonData;
            return response;
        }
    }
}