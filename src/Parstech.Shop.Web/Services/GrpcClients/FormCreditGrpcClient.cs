using Parstech.Shop.Shared.Protos.FormCredit;
using Shop.Application.DTOs.Response;
using System.Linq;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class FormCreditGrpcClient : GrpcClientBase
    {
        private readonly FormCreditService.FormCreditServiceClient _client;

        public FormCreditGrpcClient(IConfiguration configuration) : base(configuration)
        {
            _client = new FormCreditService.FormCreditServiceClient(Channel);
        }

        public async Task<ResponseDto> CreateFormCreditAsync(FormCreditDto formCredit)
        {
            var request = new CreateFormCreditRequest
            {
                FormCredit = formCredit
            };

            var response = await _client.CreateFormCreditAsync(request);

            return new ResponseDto
            {
                IsSuccessed = response.IsSuccess,
                Message = response.Message,
                Object = response.FormCredit,
                Errors = response.Errors.Select(e => new FluentValidation.Results.ValidationFailure(e.PropertyName, e.ErrorMessage)).ToList()
            };
        }

        public async Task<FormCreditDto> GetFormCreditAsync(int id)
        {
            var request = new GetFormCreditRequest { Id = id };
            return await _client.GetFormCreditAsync(request);
        }

        public async Task<List<FormCreditDto>> GetFormCreditsAsync(string filter = "", string fromDate = "", string toDate = "")
        {
            var request = new GetFormCreditsRequest
            {
                Filter = filter,
                FromDate = fromDate,
                ToDate = toDate
            };

            var response = await _client.GetFormCreditsAsync(request);
            return response.FormCredits.ToList();
        }

        public async Task<(List<FormCreditDto> Items, int TotalCount, int PageCount)> GetPagedFormCreditsAsync(int skip, int take, string filter = "", string fromDate = "", string toDate = "")
        {
            var request = new GetPagedFormCreditsRequest
            {
                Skip = skip,
                Take = take,
                Filter = filter,
                FromDate = fromDate,
                ToDate = toDate
            };

            var response = await _client.GetPagedFormCreditsAsync(request);
            
            return (
                Items: response.FormCredits.ToList(),
                TotalCount: response.TotalCount,
                PageCount: response.PageCount
            );
        }

        public async Task<ResponseDto> ChangeFormCreditStatusAsync(int id, string type)
        {
            var request = new ChangeFormCreditStatusRequest
            {
                Id = id,
                Type = type
            };

            var response = await _client.ChangeFormCreditStatusAsync(request);

            return new ResponseDto
            {
                IsSuccessed = response.IsSuccess,
                Message = response.Message
            };
        }
    }
} 