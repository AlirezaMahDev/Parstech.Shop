using MediatR;
using Shop.Application.DTOs.ProductStockPriceSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPriceSection.Requests.Queries
{
  public record GetSectionOfProductStockPriceQueryReq(int productStockPriceId):IRequest<ProdcutStockPriceSectionDto>;

}
