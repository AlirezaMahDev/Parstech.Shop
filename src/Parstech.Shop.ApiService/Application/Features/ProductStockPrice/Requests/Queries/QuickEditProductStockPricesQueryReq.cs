using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record QuickEditProductStockPricesQueryReq(string userName, List<QuickEditDto> list) : IRequest<ResponseDto>;