using MediatR;

using Parstech.Shop.Context.Application.DTOs.IRole;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Features.IRole.Requests.Commands;

public record IRoleCreateCommandReq(IRoleDto roleDto) : IRequest<Irole>;