using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
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
    public class RahkaranFollowOrderFromApiQueryHandler : IRequestHandler<RahkaranFollowOrderFromApiQueryReq, ResponseDto>
    {
        private readonly RestClient client;
        private readonly RestRequest Request;
        private readonly IMediator _mediator;

        private readonly RestRequest FollowFactor;
        public RahkaranFollowOrderFromApiQueryHandler(IMediator mediator)
        {
            client = new RestClient("https://marketing.parstech.co");
            Request = new RestRequest("/api/Rahkaran/", Method.Post);
            FollowFactor = new RestRequest("/api/Follow/", Method.Post);
            _mediator = mediator;
        }
        public async Task<ResponseDto> Handle(RahkaranFollowOrderFromApiQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto response = new ResponseDto();
            QuotationFollowUpResult result = new QuotationFollowUpResult();
            QuotationFollowUpRequest data = new QuotationFollowUpRequest();

            var RahkaranAll = await _mediator.Send(new RahakaranAllQueryReq(request.orderId));
            data.IDQ = long.Parse(RahkaranAll.order.RahkaranPishNumber);

            string JsonData = JsonConvert.SerializeObject(data);
            FollowFactor.AddJsonBody(JsonData);

            //var getTokenResult= client.Post(getTokenRequest);
            try
            {
                var finalResult = client.Post<QuotationFollowUpResult>(FollowFactor);
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
}
