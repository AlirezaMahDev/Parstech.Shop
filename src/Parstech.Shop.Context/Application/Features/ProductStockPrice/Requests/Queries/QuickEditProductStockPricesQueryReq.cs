using MediatR;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record QuickEditProductStockPricesQueryReq(string userName,List<QuickEditDto> list):IRequest<ResponseDto>;