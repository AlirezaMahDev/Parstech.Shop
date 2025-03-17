using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetSomeOfLastProductsByCateguryIdQueryReq(int Take, int CateguryId) : IRequest<List<ProductListShowDto>>;