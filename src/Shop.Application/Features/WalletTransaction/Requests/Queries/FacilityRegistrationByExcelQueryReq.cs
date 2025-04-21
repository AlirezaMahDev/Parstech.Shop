using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.WalletTransaction.Requests.Queries
{
    public record FacilityRegistrationByExcelQueryReq(string type,IFormFile file):IRequest<ResponseDto>;
}
