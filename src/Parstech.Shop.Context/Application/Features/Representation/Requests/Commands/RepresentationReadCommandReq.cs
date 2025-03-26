using MediatR;

using Parstech.Shop.Context.Application.DTOs.Representation;

namespace Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;

public record RepresentationReadCommandReq(int repId) : IRequest<RepresentationDto>;
public record RepresentationReadsCommandReq() : IRequest<List<RepresentationDto>>;