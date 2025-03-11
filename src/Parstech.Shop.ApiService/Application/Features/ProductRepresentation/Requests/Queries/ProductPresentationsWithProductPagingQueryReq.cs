using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.ProductRepresentation;

namespace Shop.Application.Features.ProductRepresentation.Requests.Queries
{
    public record ProductPresentationsWithProductPagingQueryReq(ProductRepresenationParameterDto Parameter) : IRequest<PagingDto>;

}
