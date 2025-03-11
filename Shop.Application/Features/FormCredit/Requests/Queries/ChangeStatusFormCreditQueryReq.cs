using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Response;

namespace Shop.Application.Features.FormCredit.Requests.Queries
{
    public record ChangeStatusFormCreditQueryReq(int Id,string Type):IRequest<ResponseDto>;
}