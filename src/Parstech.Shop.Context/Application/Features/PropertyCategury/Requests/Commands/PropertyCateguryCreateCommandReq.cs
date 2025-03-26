using MediatR;

using Parstech.Shop.Context.Application.DTOs.PropertyCategury;

namespace Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Commands;

public record PropertyCateguryCreateCommandReq(PropertyCateguryDto PropertyCateguryDto) : IRequest<PropertyCateguryDto>;