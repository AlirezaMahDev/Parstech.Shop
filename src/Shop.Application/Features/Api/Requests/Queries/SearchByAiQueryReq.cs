using MediatR;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Requests.Queries
{
    public record SearchByAiQueryReq(SearchReqDto parameter):IRequest<ResponseDto>;
    

}
