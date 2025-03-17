using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

public record RepresentationCreateCommandReq(RepresentationDto RepresentationDto) : IRequest<RepresentationDto>;