using MediatR;
using Shop.Application.DTOs.ProductRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Requests.Queries
{
    public record CreditProductPagingQueryReq(ProductRepresenationParameterDto ProductRepresenationParameterDto) : IRequest<ProductRepresentationPagingDto>;


}
