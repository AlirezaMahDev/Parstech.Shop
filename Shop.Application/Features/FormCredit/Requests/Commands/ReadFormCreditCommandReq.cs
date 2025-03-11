using MediatR;
using Shop.Application.DTOs.FormCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.FormCredit.Requests.Commands
{
    public record ReadFormCreditCommandReq(int Id):IRequest<FormCreditDto>;
    public record ReadFormCreditsCommandReq():IRequest<List<FormCreditDto>>;

}
