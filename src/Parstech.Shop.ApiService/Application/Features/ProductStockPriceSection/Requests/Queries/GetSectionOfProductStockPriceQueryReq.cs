using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Queries;

public record GetSectionOfProductStockPriceQueryReq(int productStockPriceId) : IRequest<ProdcutStockPriceSectionDto>;