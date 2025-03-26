using MediatR;
using Parstech.Shop.Context.Application.DTOs.Representation;

namespace Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;

public record RepresentationCreateCommandReq(RepresentationDto RepresentationDto) :IRequest<RepresentationDto>;