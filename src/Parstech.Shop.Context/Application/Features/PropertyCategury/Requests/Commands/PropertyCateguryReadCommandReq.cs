using MediatR;
using Parstech.Shop.Context.Application.DTOs.PropertyCategury;

namespace Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Commands;

public record PropertyCateguryReadCommandReq(int Id) : IRequest<PropertyCateguryDto>;
public record PropertyCateguryReadsCommandReq() : IRequest<List<PropertyCateguryDto>>;