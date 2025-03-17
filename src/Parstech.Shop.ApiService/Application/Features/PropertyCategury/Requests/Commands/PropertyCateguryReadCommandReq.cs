using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;

public record PropertyCateguryReadCommandReq(int Id) : IRequest<PropertyCateguryDto>;

public record PropertyCateguryReadsCommandReq() : IRequest<List<PropertyCateguryDto>>;