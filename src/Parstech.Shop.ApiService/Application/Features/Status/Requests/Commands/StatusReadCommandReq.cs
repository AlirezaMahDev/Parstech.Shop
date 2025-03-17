using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Status.Requests.Commands;

public record StatusReadCommandReq() : IRequest<List<StatusDto>>;