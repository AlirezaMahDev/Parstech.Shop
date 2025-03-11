using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Brand.Requests.Queries
{
    public record BrandsPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;
}
