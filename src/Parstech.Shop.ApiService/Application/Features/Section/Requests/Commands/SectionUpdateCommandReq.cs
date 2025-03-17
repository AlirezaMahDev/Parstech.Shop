using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;

public record SectionUpdateCommandReq(SectionDto SectionDto) : IRequest<SectionDto>;