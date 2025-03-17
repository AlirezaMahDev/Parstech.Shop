using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Commands;

public record PropertyReadCommandReq(int id) : IRequest<PropertyDto>;