using MediatR;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Requests.Queries
{
    public record QuickEditProductStockPricesQueryReq(string userName,List<QuickEditDto> list):IRequest<ResponseDto>;
}
