using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.IRole.Requests.Commands;

public record IRoleReadAllCommandReq() : IRequest<List<IRoleDto>>;

public record IRoleReadCommandReq(string id) : IRequest<IRoleDto>;