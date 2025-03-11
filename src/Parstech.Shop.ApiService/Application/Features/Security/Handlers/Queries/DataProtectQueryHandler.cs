using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Security.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Security.Handlers.Queries
{
    public class DataProtectQueryHandler : IRequestHandler<DataProtectQueryReq, ResponseDto>
    {
        private readonly IDataProtectionProvider _protectionProvider;
        public DataProtectQueryHandler(IDataProtectionProvider protectionProvider)
        {
            _protectionProvider = protectionProvider;
        }
        
        public async Task<ResponseDto> Handle(DataProtectQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto response = new ResponseDto();
            var _dataProtector = _protectionProvider.CreateProtector("onlineShopKey");
            var _timeLimitDataPotector = _dataProtector.ToTimeLimitedDataProtector();
            if (request.type == "protect")
            {
                
                var _protectdValue = _timeLimitDataPotector.Protect(request.value, TimeSpan.FromDays(1));
                response.IsSuccessed = true;
                response.Object = _protectdValue.ToString();
            }
            else
            {
                try
                {
                    var _UnprotectdValue = _timeLimitDataPotector.Unprotect(request.value);
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
}
