using MediatR;
using Newtonsoft.Json;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Api.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shop.Application.Features.Api.Handlers.Queries
{
    
    public class SearchByAiQueryHandler : IRequestHandler<SearchByAiQueryReq, ResponseDto>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SearchByAiQueryHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto> Handle(SearchByAiQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto result = new ResponseDto();

            
            var client = _httpClientFactory.CreateClient();
            
            var uriBuilder = new UriBuilder("http://localhost:8000/search");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["query"] = request.parameter.Filter;
            query["top_k"] = request.parameter.Top_k.ToString();
            uriBuilder.Query = query.ToString();

            
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);

            try
            {
                var response = await client.SendAsync(httpRequest, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);
                    var contentRes = JsonConvert.DeserializeObject<SearchResDto>(content);
                    result.IsSuccessed = true;
                    result.Object = contentRes;
                }
                else
                {
                    result.IsSuccessed = false;
                    result.Message = "وب سرویس با شکست مواجه شده است.";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessed = false;
                result.Message = $"خطا در ارتباط با سرویس: {ex.Message}";
            }





            result.IsSuccessed = false;
            result.Message = "وب سرویس احراز هویت بانک با شکست مواجه شده است. ";
            return result;
        }
    }


}


