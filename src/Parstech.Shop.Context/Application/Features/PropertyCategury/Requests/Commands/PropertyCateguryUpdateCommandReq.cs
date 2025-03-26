using MediatR;

using Parstech.Shop.Context.Application.DTOs.PropertyCategury;

namespace Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Commands;

public record PropertyCateguryUpdateCommandReq(PropertyCateguryDto PropertyCateguryDto) : IRequest<PropertyCateguryDto>;