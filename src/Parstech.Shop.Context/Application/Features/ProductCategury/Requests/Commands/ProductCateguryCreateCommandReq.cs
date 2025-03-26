using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

public record ProductCateguryCreateCommandReq(ProductCateguryDto ProductCateguryDto) : IRequest<ProductCateguryDto>;