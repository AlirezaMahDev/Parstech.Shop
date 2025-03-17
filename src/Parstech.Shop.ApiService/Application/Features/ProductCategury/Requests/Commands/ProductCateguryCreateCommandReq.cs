using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryCreateCommandReq(ProductCateguryDto ProductCateguryDto) : IRequest<ProductCateguryDto>;