using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Commands;

public record CreateProdcutStockPriceSectionCommandReq(int productStockPriceId,int sectionId) : IRequest<ResponseDto>;