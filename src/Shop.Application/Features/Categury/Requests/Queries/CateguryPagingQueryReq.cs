using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;

namespace Shop.Application.Features.Categury.Requests.Queries
{
    public record CateguryPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;

}
