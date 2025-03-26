using MediatR;
using Parstech.Shop.Context.Application.DTOs.Property;

namespace Parstech.Shop.Context.Application.Features.Property.Requests.Commands;

public record AllPropertiesReadCommandReq : IRequest<List<PropertyDto>>;