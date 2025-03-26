using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductCategury;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Queries;

public record CateguryOfProductQueryReq(int productId) : IRequest<ProductCateguryDto>;