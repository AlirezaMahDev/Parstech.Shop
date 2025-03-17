using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Queries;

public record CateguriesOfProductQueryReq(int productId) : IRequest<List<ProductCateguryDto>>;