using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.State.Requests.Commands;

public record StatesReadsCommandReq() : IRequest<List<SteteDto>>;