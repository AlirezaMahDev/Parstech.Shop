using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record DargaQueryReq(string orderCode, int transactionId, long price) : IRequest<string>;

public record ZarrinPalQueryReq(string orderCode, int transactionId) : IRequest<string>;

public record NoPayQueryReq(string orderCode, int transactionId, long price) : IRequest<string>;