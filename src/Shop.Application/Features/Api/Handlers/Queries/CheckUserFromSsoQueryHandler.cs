using MediatR;
using Newtonsoft.Json;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Handlers.Queries
{
    public class CheckUserFromSsoQueryhandler : IRequestHandler<CheckUserFromSsoQueryReq, ResponseDto>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CheckUserFromSsoQueryhandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto> Handle(CheckUserFromSsoQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto result=new ResponseDto();
           
            var path = $"https://refahi.parstechnology.ir/api/sso/residence/check_user?nationalNumber={request.nationalCode}";
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Token", "9bb4210d1881b3683c7648d8a4f288ad");
            var response = await client.PostAsync(path,null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var sso=JsonConvert.DeserializeObject<SsoDto>(content);
                result.IsSuccessed = true;
                result.Object = sso;
                return result;
            }

            result.IsSuccessed = false;
            result.Message = "وب سرویس احراز هویت بانک با شکست مواجه شده است. ";
            return result;
        }
    }
}
