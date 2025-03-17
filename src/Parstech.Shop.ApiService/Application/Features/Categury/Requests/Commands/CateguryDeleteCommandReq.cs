using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Requests.Commands;

public record CateguryDeleteCommandReq(int categuryId) : IRequest<ResponseDto>;