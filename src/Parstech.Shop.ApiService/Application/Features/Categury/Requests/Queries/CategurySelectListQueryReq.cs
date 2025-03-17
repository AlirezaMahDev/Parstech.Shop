using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;

public record CategurySelectListQueryReq() : IRequest<List<CategurySelectDto>>;