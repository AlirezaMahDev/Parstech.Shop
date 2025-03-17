using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.State.Requests.Commands;

public record StatesReadsCommandReq() : IRequest<List<SteteDto>>;