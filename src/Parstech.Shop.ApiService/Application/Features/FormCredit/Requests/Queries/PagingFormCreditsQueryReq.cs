using MediatR;
using Shop.Application.DTOs.FormCredit;
using Shop.Application.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.FormCredit.Requests.Queries
{
    public record PagingFormCreditsQueryReq(ParameterDto Parameter) :IRequest<List<FormCreditDto>>;
}
