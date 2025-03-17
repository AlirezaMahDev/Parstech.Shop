using MediatR;

using Microsoft.AspNetCore.DataProtection;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Security.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Security.Handlers.Queries;

public class DataProtectQueryHandler : IRequestHandler<DataProtectQueryReq, ResponseDto>
{
    private readonly IDataProtectionProvider _protectionProvider;

    public DataProtectQueryHandler(IDataProtectionProvider protectionProvider)
    {
        _protectionProvider = protectionProvider;
    }

    public async Task<ResponseDto> Handle(DataProtectQueryReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        IDataProtector _dataProtector = _protectionProvider.CreateProtector("onlineShopKey");
        ITimeLimitedDataProtector _timeLimitDataPotector = _dataProtector.ToTimeLimitedDataProtector();
        if (request.type == "protect")
        {
            string _protectdValue = _timeLimitDataPotector.Protect(request.value, TimeSpan.FromDays(1));
            response.IsSuccessed = true;
            response.Object = _protectdValue.ToString();
        }
        else
        {
            try
            {
                string _UnprotectdValue = _timeLimitDataPotector.Unprotect(request.value);
                response.IsSuccessed = true;
                response.Object = _UnprotectdValue.ToString();
            }
            catch (Exception ex)
            {
                response.IsSuccessed = false;
            }
        }


        return response;
    }
}