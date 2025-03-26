using MediatR;

using Parstech.Shop.Context.Application.DTOs.IRole;

namespace Parstech.Shop.Context.Application.Features.IRole.Requests.Commands;

public record IRoleReadAllCommandReq() : IRequest<List<IRoleDto>>;
public record IRoleReadCommandReq(string id) : IRequest<IRoleDto>;