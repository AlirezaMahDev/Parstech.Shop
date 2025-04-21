using MediatR;
using Shop.Application.DTOs.FormCredit;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.FormCredit.Requests.Commands
{
    public record CreateFormCreditCommandReq(FormCreditDto FormCreditDto):IRequest<ResponseDto>;

}
