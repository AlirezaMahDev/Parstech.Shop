using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;

public record IRoleCreateCommandReq(IRoleDto roleDto) : IRequest<Irole>;