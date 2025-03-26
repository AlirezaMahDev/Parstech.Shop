using MediatR;

using Parstech.Shop.Context.Application.DTOs.Status;

namespace Parstech.Shop.Context.Application.Features.Status.Requests.Commands;

public record StatusReadCommandReq() : IRequest<List<StatusDto>>;