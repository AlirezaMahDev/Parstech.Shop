using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Property;

namespace Shop.Application.Features.Property.Requests.Queries
{
    public record PropertyPagingQueryReq(PropertyParameterDto Parameter) : IRequest<PagingDto>;

}
