using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Queries;

public record CateguryOfProductQueryReq(int productId) : IRequest<ProductCateguryDto>;