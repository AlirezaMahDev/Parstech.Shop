using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Commands;

public record DeleteProdcutStockPriceSectionCommandReq(int id) : IRequest<ResponseDto>;