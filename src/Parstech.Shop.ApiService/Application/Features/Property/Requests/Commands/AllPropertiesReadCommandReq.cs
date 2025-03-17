using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

public record AllPropertiesReadCommandReq : IRequest<List<PropertyDto>>;