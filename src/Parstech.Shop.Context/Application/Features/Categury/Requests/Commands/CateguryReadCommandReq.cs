using MediatR;
using Parstech.Shop.Context.Application.DTOs.Categury;

namespace Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;

public record CateguryOneReadCommandReq(int categuryId) : IRequest<CateguryDto>;
public record CateguryReadCommandReq(string filter) : IRequest<List<CateguryDto>>;