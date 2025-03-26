using MediatR;

using Parstech.Shop.Context.Application.DTOs.State;

namespace Parstech.Shop.Context.Application.Features.State.Requests.Commands;

public record StatesReadsCommandReq():IRequest<List<SteteDto>>;