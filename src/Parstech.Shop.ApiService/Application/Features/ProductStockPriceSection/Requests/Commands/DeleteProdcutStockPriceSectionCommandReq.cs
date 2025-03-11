using MediatR;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPriceSection.Requests.Commands
{
    public record DeleteProdcutStockPriceSectionCommandReq(int id):IRequest<ResponseDto>;

}
