using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

public record RepresentationReadCommandReq(int repId) : IRequest<RepresentationDto>;

public record RepresentationReadsCommandReq() : IRequest<List<RepresentationDto>>;