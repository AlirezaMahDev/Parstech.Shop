using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

public record RepresentationCreateCommandReq(RepresentationDto RepresentationDto) : IRequest<RepresentationDto>;