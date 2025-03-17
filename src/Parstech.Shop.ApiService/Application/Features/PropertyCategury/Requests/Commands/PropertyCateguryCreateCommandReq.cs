using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;

public record PropertyCateguryCreateCommandReq(PropertyCateguryDto PropertyCateguryDto) : IRequest<PropertyCateguryDto>;