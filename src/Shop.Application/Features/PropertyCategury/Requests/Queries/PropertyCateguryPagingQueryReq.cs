using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;

namespace Shop.Application.Features.PropertyCategury.Requests.Queries
{
    public record PropertyCateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;

}
