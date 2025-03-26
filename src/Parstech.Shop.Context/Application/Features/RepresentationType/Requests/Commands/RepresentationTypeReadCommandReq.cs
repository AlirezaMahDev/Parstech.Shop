using MediatR;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;

namespace Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;

public record RepresentationTypeReadCommandReq(int RepTypeId) : IRequest<RepresentationTypeDto>;
public record RepresentationTypeReadsCommandReq() : IRequest<List<RepresentationTypeDto>>;