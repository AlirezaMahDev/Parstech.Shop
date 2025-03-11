using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Security.Requests.Queries
{
    public record DataProtectQueryReq(string value,string type):IRequest<ResponseDto>;

}
