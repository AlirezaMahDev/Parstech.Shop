using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductStockPriceSection;

namespace Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Queries;

public record GetSectionOfProductStockPriceQueryReq(int productStockPriceId):IRequest<ProdcutStockPriceSectionDto>;