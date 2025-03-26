using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Commands;

public record DeleteProdcutStockPriceSectionCommandReq(int id):IRequest<ResponseDto>;