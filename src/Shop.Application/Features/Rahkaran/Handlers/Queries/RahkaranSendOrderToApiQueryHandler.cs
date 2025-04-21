using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using RestSharp;
using Shop.Application.Calculator;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Rahkaran.Requests.Queries;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Rahkaran.Handlers.Queries
{
    public class RahkaranSendOrderToApiQueryHandler : IRequestHandler<RahkaranSendOrderToApiQueryReq, ResponseDto>
    {
        private readonly RestClient client;
        private readonly RestRequest Request;
        private readonly IMediator _mediator;
        
        private readonly RestRequest FollowFactor;
        public RahkaranSendOrderToApiQueryHandler(IMediator mediator)
        {
            client = new RestClient("https://marketing.parstech.co");
            //client = new RestClient("http://172.17.1.120:8081");
            Request = new RestRequest("/api/Rahkaran/", Method.Post);
            FollowFactor = new RestRequest("/api/Follow/", Method.Post);
            _mediator = mediator;
        }
        public async Task<ResponseDto> Handle(RahkaranSendOrderToApiQueryReq request, CancellationToken cancellationToken)
        {
            var RahkaranAll =await _mediator.Send(new RahakaranAllQueryReq(request.orderId));
            ResponseDto response = new ResponseDto();
           
            List<RakaranProductItem> items = new List<RakaranProductItem>();

            
            
            foreach (var detail in RahkaranAll.products)
            {
                int? unitId = detail.RahkaranUnitId;
                long oneProductPrice = detail.Price.Value / detail.Count.Value;
                long Price=PersentCalculator.RahkaranCalvulatorByPrice(oneProductPrice);
                RakaranProductItem item = new RakaranProductItem()
                {
                    ProductId = detail.RahkaranProductId.ToString().Trim(),
                    UnitId = unitId.ToString().Trim(),
                    Fee = Price.ToString().Trim(),
                    Quantity = detail.Count.ToString().Trim(),
                };
                items.Add(item);
            }


            RahkaranDto data = new RahkaranDto();

            data = new RahkaranDto()
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
                var finalResult = client.Post<RahkaranResult>(Request);
                if (finalResult.QuotationId != "-")
                {
                    response.IsSuccessed = true;
                    response.Message = "سفارش با موفقیت در سامانه راهکاران ثبت گردید";
                   
                    response.Object2 = finalResult.Messages;
                    RahkaranAll.order.RahkaranPishNumber= finalResult.QuotationId;
                    await _mediator.Send(new RahkaranOrderUpdateCommandReq(RahkaranAll.order));

                }
                else
                {
                    response.IsSuccessed =false;
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
}
