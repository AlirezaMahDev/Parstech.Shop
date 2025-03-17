using MediatR;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;

public record IRoleCreateCommandReq(IRoleDto roleDto) : IRequest<Irole>;