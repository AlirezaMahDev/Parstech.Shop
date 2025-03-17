using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Commands;

public record CreateProdcutStockPriceSectionCommandReq(int productStockPriceId, int sectionId) : IRequest<ResponseDto>;