using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductCategury;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;

public record CateguriesOfProductQueryReq(int productId) : IRequest<List<ProductCateguryDto>>;