using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

public record RepresentationReadCommandReq(int repId) : IRequest<RepresentationDto>;

public record RepresentationReadsCommandReq() : IRequest<List<RepresentationDto>>;