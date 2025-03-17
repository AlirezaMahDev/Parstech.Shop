using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.RepresentationType.Requests.Commands;

public record RepresentationTypeReadCommandReq(int RepTypeId) : IRequest<RepresentationTypeDto>;

public record RepresentationTypeReadsCommandReq() : IRequest<List<RepresentationTypeDto>>;