using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;

public record CateguryOneReadCommandReq(int categuryId) : IRequest<CateguryDto>;

public record CateguryReadCommandReq(string filter) : IRequest<List<CateguryDto>>;